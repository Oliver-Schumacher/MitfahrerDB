using Microsoft.AspNetCore.Mvc;


namespace MitfahrerDB_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger;
        private DataBaseContext _db = new DataBaseContext();
        
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
        public IActionResult Post(string user, string email, string password)
        {
            //TODO return jwt token?
            //return null;

            if (CheckName(user))
            {
                return BadRequest("The user already exists");
            }
            
            if (CheckMail(email))
            {
                return BadRequest("Mail is not a GSO Mail");
            }

            
            //TODO Check Password
            return Ok();
        }

        [NonAction]
        private bool CheckMail(string mailAddress)
        {
            return !mailAddress.ToLower().EndsWith("@gso.schule.koeln");
        }
        
        [NonAction]
        private bool CheckName(string name)
        {
            var user = _db.Users.FirstOrDefault(u => u.Name.ToLower() == name.ToLower());
            return user is not null;
        }
    }
}