using Microsoft.AspNetCore.Mvc;


namespace MitfahrerDB_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger;
        private readonly DataBaseContext _db = new DataBaseContext();
        
        public RegistrationController(ILogger<RegistrationController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetRegistration")]
        public string Get()
        {
            return "Hello World";
            //TODO get genders that are available in database
        }
        
        [HttpPost(Name = "PostRegistration")]
        public IActionResult Post(string userName, string email, string password, int genderId)
        {
            //TODO return jwt token?
            //return null;

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

            User user = new User
            {
                Name = userName,
                Mail = email,
                GenderId = genderId,
                Passwort = password
            };
            _db.Users.Add(user);
            _db.SaveChanges();
 
            return Ok();
        }

        [NonAction]
        private (bool success, string message) CheckMail(string mailAddress)
        {
            if (!mailAddress.ToLower().EndsWith("@gso.schule.koeln")) return (false, "Mail must end with @gso.schule.koeln");

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
    }
}