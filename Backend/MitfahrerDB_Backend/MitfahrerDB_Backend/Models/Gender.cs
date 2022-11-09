using System.ComponentModel.DataAnnotations;

namespace MitfahrerDB_Backend.Models;

public class Gender
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}