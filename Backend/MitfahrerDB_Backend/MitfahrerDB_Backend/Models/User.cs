using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MitfahrerDB_Backend.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Mail { get; set; }
    public string Passwort { get; set; }
    public string? Phone { get; set; }
    public int GenderId { get; set; }

    [ForeignKey("GenderId")]
    public virtual Gender Gender { get; set; }
}