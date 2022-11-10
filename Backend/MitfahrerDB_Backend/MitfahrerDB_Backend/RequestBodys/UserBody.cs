namespace MitfahrerDB_Backend.RequestBodys
{
    public class UserBody
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Passwort { get; set; }
        public string? Phone { get; set; }
        public int GenderId { get; set; }
    }
}
