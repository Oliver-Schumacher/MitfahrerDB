using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using MitfahrerDB_Backend.Models;

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
        public IActionResult UserLogin(string Email = "", string Password = "")
        {
            if (VerifyUser(Email, Password))
            {
                return Ok(GenerateToken(Email));
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
            if ((email != string.Empty && CheckMail(email).success) && (password != string.Empty))
            {
                User? user = _db.Users.FirstOrDefault(u => u.Mail.ToLower() == email.ToLower() && u.Passwort.ToLower() == password.ToLower());
                if (user is not null)
                    return true;
            }
            return false;
        }

        #endregion login

        #region registration

        [HttpGet("Registration")]
        public List<Gender> Get()
        {
            return _db.Genders.ToList();
        }

        [HttpPost("Registration")]
        public IActionResult Post(string userName, string email, string password, int genderId, string phone)
        {
            var userResult = CheckName(userName);
            if (!userResult.success)
            {
                return BadRequest(userResult.message);
            }

            var mailResult = CheckMail(email);
            if (!mailResult.success)
            {
                return BadRequest(mailResult.message);
            }

            var user = new User
            {
                Name = userName,
                Mail = email,
                GenderId = genderId,
                Passwort = password,
                Phone = phone
            };
            _db.Users.Add(user);
            var rowsInserted = _db.SaveChanges();
            if (rowsInserted == 0) return BadRequest("The User could not be written into the Database");
            return Ok();
        }

        [NonAction]
        private (bool success, string message) CheckMail(string mailAddress)
        {
            if (!mailAddress.ToLower().EndsWith(emailServerExtension)) return (false, $"Mail must end with {emailServerExtension}.");

            User? user = _db.Users.FirstOrDefault(u => u.Mail.ToLower() == mailAddress.ToLower());
            if (user is not null) return (false, $"Mail {mailAddress} Already exists.");

            return (true, "");
        }

        [NonAction]
        private (bool success, string message) CheckName(string name)
        {
            User? user = _db.Users.FirstOrDefault(u => u.Name.ToLower() == name.ToLower());
            if (user is not null) return (false, $"The User {name} already exists.");

            return (true, "");
        }
        #endregion registration

        [HttpGet("Profile")]
        public IActionResult GetProfile(int UserId)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == UserId);
            if (user is null) return BadRequest("User not found");
            return Ok(user);
        }

        [HttpPost("Profile")]
        public IActionResult UpdateProfile(int UserId, string Name, string Mail, int GenderId, string Phone)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == UserId);
            if (user != null)
            {
                if (Name != String.Empty && user.Name != Name)
                    user.Name = Name;
                if ((Mail != String.Empty && user.Mail != Mail) && (CheckMail(Mail).success))
                    user.Mail = Mail;
                if (user.GenderId != GenderId)
                    user.GenderId = GenderId;
                if (Phone != String.Empty && user.Phone != Phone)
                    user.Phone = Phone;

                _db.Users.Update(user);
                var updatedrows = _db.SaveChanges();
                if (updatedrows == 0)
                    return BadRequest("The user could not be updated!");
                return Ok("User saved");
            }
            return BadRequest("UserId was not found!");
        }
    }
}
