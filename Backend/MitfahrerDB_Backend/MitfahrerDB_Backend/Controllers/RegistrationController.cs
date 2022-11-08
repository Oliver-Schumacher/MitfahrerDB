using Microsoft.AspNetCore.Mvc;


namespace MitfahrerDB_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger;
        private readonly DataBaseContext _db = new DataBaseContext();
        private const string emailServerExtension = "@gso.schule.koeln";
        
        public RegistrationController(ILogger<RegistrationController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetRegistration")]
        public List<Gender> Get()
        {
            return _db.Genders.ToList();
        }
        
        [HttpPost(Name = "PostRegistration")]
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
    }
}