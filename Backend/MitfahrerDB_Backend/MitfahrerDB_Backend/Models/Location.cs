using System.ComponentModel.DataAnnotations;

namespace MitfahrerDB_Backend.Models;

public class Location
{
    [Key]
    public int Id { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
}