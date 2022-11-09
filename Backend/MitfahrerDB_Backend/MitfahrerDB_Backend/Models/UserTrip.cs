using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MitfahrerDB_Backend.Models;

public class UserTrip
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TripId { get; set; }
    
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
    
    [ForeignKey("TripId")]
    public virtual Trip Trip { get; set; }
}