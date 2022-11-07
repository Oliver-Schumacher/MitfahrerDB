namespace MitfahrerDB_Backend
{
    public class LoginUser
    {
        public string EmailAdress { get; set;}
        public string Password { get; set; }

        public string Username { get; set; }
        public LoginUser(string emailAdress, string password, string username)
        {
            EmailAdress = emailAdress;
            Password = password;
            Username = username;
        }
        public LoginUser()
        {
            EmailAdress = "";
            Password = "";
            Username = "";
        }
    }
}
