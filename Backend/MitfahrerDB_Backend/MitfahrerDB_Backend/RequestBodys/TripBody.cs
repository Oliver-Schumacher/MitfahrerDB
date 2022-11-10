namespace MitfahrerDB_Backend.RequestBodys
{
    public class TripBody
    {
        public string LocationStartLat { get; set; }
        public string LocationStartLon { get; set; }
        public string LocationEndLat { get; set; }
        public string LocationEndLon { get; set; }
        public int DriverId { get; set; }
        public int Lesson { get; set; }
        public bool SameGender { get; set; }
        public int AvailableSeats { get; set; }
        public string Address { get; set; }
        public string WeekDay { get; set; }
        public int ToGSO { get; set; }
    }
}
