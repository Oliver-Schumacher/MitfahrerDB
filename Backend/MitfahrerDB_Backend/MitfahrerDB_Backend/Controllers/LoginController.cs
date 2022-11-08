using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SQLitePCL;
using System.IdentityModel.Tokens.Jwt;
using System.Security;

namespace MitfahrerDB_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly DataBaseContext _db = new DataBaseContext();

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Wandelt die übergebenen Eingaben vom User um, um diese im Anschluss zu Prüfen und zu responsen. 
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        [HttpPost(Name = "PostLogin")]
        public JwtSecurityToken UserLogin(string Email, string Password)
        {
            JwtSecurityToken token;
            //Übergebenen User, danach Prüfen ob dieser Login gültig ist. 
            if (VerifyUser(Email, Password))
                token = new JwtSecurityToken("MitfahrerDB", "UserLogin", null, DateTime.Now, DateTime.Now.AddHours(2), null);
            else
                token = new JwtSecurityToken("MitfahrerDB", "UserLogin", null, null, null, null);

            return token ;
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
    }
}
