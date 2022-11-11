using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using MitfahrerDB_Backend.Models;
using MitfahrerDB_Backend.RequestBodys;

namespace MitfahrerDB_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly DataBaseContext _db = new DataBaseContext();
        private const string emailServerExtension = "@gso.schule.koeln";

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        #region login

        /// <summary>
        /// Generiert und liefert einen JwtSecurityToken zurück. 
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        [NonAction]
        public JwtSecurityToken GenerateToken(string mail)
        {
            var mySecret = "MitfahrerDB_Secret";
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            var myIssuer = "MitfahrerDB_Issuer";
            var myAudience = "MitfahrerDB_Audience";

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.NameIdentifier, mail),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = myIssuer,
                Audience = myAudience,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            return (JwtSecurityToken)tokenHandler.CreateToken(tokenDescriptor);
        }

        /// <summary>
        /// Wandelt die übergebenen Eingaben vom User um, um diese im Anschluss zu Prüfen und zu responsen. 
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public IActionResult UserLogin([FromBody]UserBody? _User)
        {
            if (VerifyUser(_User.Mail, _User.Passwort))
            {
                return Ok(GenerateToken(_User.Mail));
            }
            return BadRequest("Login failed!");
        }

        /// <summary>
        /// Überprüfe den übergebenen User, ob dieser in der Datenbank vorhanden ist. 
        /// </summary>
        /// <returns></returns>
        [NonAction]
        private bool VerifyUser(string email, string password)
        {
            if ((email != string.Empty && VerifyMail(email).success) && (password != string.Empty))
            {
                User? user = _db.Users.FirstOrDefault(u => u.Mail.ToLower() == email.ToLower() && u.Passwort.ToLower() == password.ToLower());
                if (user is not null)
                    return true;
            }
            return false;
        }

        #endregion login

        #region registration

        /// <summary>
        /// Liefert eine Liste der Genders zurück. 
        /// </summary>
        /// <returns></returns>
        [HttpGet("Registration")]
        public List<Gender> Get()
        {
            return _db.Genders.ToList();
        }

        /// <summary>
        /// Methode zum Anlegen eines neuen Users in der Datenbank, falls die eingegebenen Daten Vollständig und noch nicht verwendet werden. 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="genderId"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        [HttpPost("Registration")]
        public IActionResult Post([FromBody]UserBody? _newUser)
        {
            if (_newUser.Name is null || _newUser.Name == String.Empty)
                return BadRequest("User Name is required.");
            var userResult = CheckName(_newUser.Name);
            if (!userResult.success)
            {
                return BadRequest(userResult.message);
            }

            var mailResult = CheckMail(_newUser.Mail);
            if (!mailResult.success)
            {
                return BadRequest(mailResult.message);
            }

            var user = new User
            {
                Name = _newUser.Name,
                Mail = _newUser.Mail,
                GenderId = _newUser.GenderId,
                Passwort = _newUser.Passwort,
                Phone = _newUser.Phone
            };
            _db.Users.Add(user);
            var rowsInserted = _db.SaveChanges();
            if (rowsInserted == 0) return BadRequest("The User could not be written into the Database");
            return Ok();
        }

        /// <summary>
        /// Überprüft ob die angegebene Mail schon in verwendung ist.
        /// </summary>
        /// <param name="mailAddress"></param>
        /// <returns></returns>
        [NonAction]
        private (bool success, string message) CheckMail(string mailAddress)
        {
            if (!mailAddress.ToLower().EndsWith(emailServerExtension)) return (false, $"Mail must end with {emailServerExtension}.");

            User? user = _db.Users.FirstOrDefault(u => u.Mail.ToLower() == mailAddress.ToLower());
            if (user is not null) return (false, $"Mail {mailAddress} Already exists.");

            return (true, "");
        }

        /// <summary>
        /// Überprüft ob die angegebene Mail eine Valide E-Mail zu einem User ist.
        /// </summary>
        /// <param name="mailAdress"></param>
        /// <returns></returns>
        [NonAction]
        private (bool success, string message) VerifyMail(string mailAdress)
        {
            if (!mailAdress.ToLower().EndsWith(emailServerExtension)) 
                return (false, $"Mail must end with {emailServerExtension}");
            User? user = _db.Users.FirstOrDefault(u => u.Mail.ToLower() == mailAdress.ToLower());
            if (user is null)
                return (false, "User not found!");
            return (true, "");
        }
        /// <summary>
        /// Überprüft ob der Name, des Users schon in Verwendung ist.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [NonAction]
        private (bool success, string message) CheckName(string name)
        {
            if (name is null || name == String.Empty)
                return (false, "Name is required!");
            User? user = _db.Users.FirstOrDefault(u => u.Name.ToLower() == name.ToLower());
            if (user is not null) return (false, $"The User {name} already exists.");

            return (true, "");
        }
        #endregion registration

        /// <summary>
        /// Liefert ein User Profil zurück, anhand der übergebenen UserId 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet("Profile")]
        public IActionResult GetProfile([FromBody]UserBody? _user)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == _user.Id);
            if (user is null) return BadRequest("User not found");
            return Ok(user);
        }

        /// <summary>
        /// Updated das User Profil anhand der neu angegeben Daten, vorrausgesetzt diese sind Valide. 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Name"></param>
        /// <param name="Mail"></param>
        /// <param name="GenderId"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        [HttpPost("Profile")]
        public IActionResult UpdateProfile([FromBody]UserBody? _user)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == _user.Id);
            if (user != null)
            {
                if (_user.Name != String.Empty && user.Name != _user.Name)
                    user.Name = _user.Name;
                if ((_user.Mail != String.Empty && user.Mail != _user.Mail))
                    user.Mail = _user.Mail;
                if (user.GenderId != _user.GenderId)
                    user.GenderId = _user.GenderId;
                if (_user.Phone != String.Empty && user.Phone != _user.Phone)
                    user.Phone = _user.Phone;

                _db.Users.Update(user);
                var updatedrows = _db.SaveChanges();
                if (updatedrows == 0)
                    return BadRequest("The user could not be updated!");
                return Ok("User saved");
            }
            return BadRequest("UserId was not found!");
        }

        [HttpGet("/User/{id}/Trips")]
        public IActionResult GetUserTrips(int id)
        {
            var trips = _db.Trips.Where(t => t.DriverId == id).ToList();
            return Ok(trips);
        }
    }
}
