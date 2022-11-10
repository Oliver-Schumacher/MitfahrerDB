namespace MitfahrerDB_Backend.RequestBodys
{
    public class TripBody
    {
        public int LocationStartId { get; set; }
        public int LocationEndId { get; set; }
        public int DriverId { get; set; }
        public string StartTime { get; set; }
        public bool SameGender { get; set; }
        public int AvailableSeats { get; set; }
        public string Address { get; set; }
        public string WeekDay { get; set; }
        public int ToGSO { get; set; }
    }
}
