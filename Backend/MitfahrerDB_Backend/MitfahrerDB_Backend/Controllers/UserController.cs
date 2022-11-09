using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        public JwtSecurityToken UserLogin(string Email = "", string Password = "")
        {
            if (VerifyUser(Email, Password))
            {
                return GenerateToken(Email);
            }
            return GenerateToken(Email);
        }

        /// <summary>
        /// Überprüfe den übergebenen User, ob dieser in der Datenbank vorhanden ist. 
        /// </summary>
        /// <returns></returns>
        [NonAction]
        private bool VerifyUser(string email, string password)
        {
            if ((email != string.Empty && VerifyEmailAdress(email)) && (password != string.Empty))
            {
                var user = _db.Users.FirstOrDefault(u => u.Mail.ToLower() == email.ToLower() && u.Passwort.ToLower() == password.ToLower());
                if (user is not null)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Verifiziert die Übergebene E-Mail-Adresse. 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [NonAction]
        private bool VerifyEmailAdress(string email)
        {
            if (email.Contains("@gso.schule.koeln"))
                return true;
            return false;
        }

        #endregion login

        #region registration

        [HttpGet(Name = "GetRegistration")]
        public List<Gender> Get()
        {
            return _db.Genders.ToList();
        }

        [HttpPost("Registration")]
        public IActionResult Post(string userName, string email, string password, int genderId)
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
                Passwort = password
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

            var user = _db.Users.FirstOrDefault(u => u.Mail.ToLower() == mailAddress.ToLower());
            if (user is not null) return (false, $"Mail {mailAddress} Already exists.");

            return (true, "");
        }

        [NonAction]
        private (bool success, string message) CheckName(string name)
        {
            var user = _db.Users.FirstOrDefault(u => u.Name.ToLower() == name.ToLower());
            if (user is not null) return (false, $"The User {name} already exists.");

            return (true, "");
        }
        #endregion registration

        [HttpGet("Profile{UserId}")]
        public List<string> GetProfile(int UserId)
        {
            List<string> userInformation = new List<string>();
            if (UserId != null)
            {
                var user = _db.Users.FirstOrDefault(u => u.Id == UserId);
                if (user != null)
                {
                    userInformation.Add("Name: " + user.Name.ToString());
                    userInformation.Add("Mail: " + user.Mail.ToString());
                    userInformation.Add("GenderId: " + user.GenderId.ToString());
                    //userInformation.Add("Phonenumber:" + user.PhoneNumber.ToString());
                }
            }
            return userInformation;
        }
        //[HttpPost("Profile/Update{UserId}{Name}{Mail}{GenderId}{Phonenumber}")]
        //public IActionResult UpdateProfile(int userid, string name, string Mail, int GenderId, string phonenumber)
        //{
        //    if (userid != null)
        //    {
        //        var user = _db.Users.FirstOrDefault(u => u.Id == userid);
        //        if (user != null)
        //        {
        //            if (name != String.Empty && user.Name != name)
        //                user.Name = name;
        //            if (Mail != String.Empty && user.Mail != Mail)
        //                user.Mail = Mail;
        //            if (GenderId != null && user.GenderId != GenderId)
        //                user.GenderId = GenderId;
        //            //if (phonenumber != String.Empty && user.phonenumber != phonenumber)
        //            //    user.phonenumber = phonenumber;
        //            return Ok();
        //        }
        //    }
        //    return BadRequest();
        //}
    }
}
