using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MitfahrerDB_Backend.Models;

public class Trip
{
    [Key]
    public int Id { get; set; }
    public int LocationStartId { get; set; }
    public int LocationEndId { get; set; }
    public int DriverId { get; set; }
    public string StartTime { get; set; }
    public bool SameGender { get; set; }
    public int AvailableSeats { get; set; }
    public string Address { get; set; }
    public string WeekDay { get; set; }
    public int ToGSO { get; set; }
    
    [ForeignKey("LocationStartId")]
    public virtual Location LocationStart { get; set; }
    
    [ForeignKey(("LocationEndId"))]
    public virtual Location LocationEnd { get; set; }
    
    [ForeignKey("DriverId")]
    public virtual User Driver { get; set; }
}