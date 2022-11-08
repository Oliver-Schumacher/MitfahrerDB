using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MitfahrerDB_Backend;

public class DataBaseContext : DbContext
{
    public DbSet<Gender> Genders { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<UserTrip> UserTrips { get; set; }
    public DbSet<Location> Locations { get; set; }

    private string DbPath { get; }

    public DataBaseContext()
    {
        DbPath = Path.Join(Directory.GetCurrentDirectory(), "mitfahrer.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }
}

public class Gender
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}

public class User
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Mail { get; set; }
    public string Passwort { get; set; }
    public int GenderId { get; set; }
    
    [ForeignKey("GenderId")]
    public virtual Gender Gender { get; set; }
}

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
    
    [ForeignKey("LocationStartId")]
    public virtual Location LocationStart { get; set; }
    
    [ForeignKey(("LocationEndId"))]
    public virtual Location LocationEnd { get; set; }
    
    [ForeignKey("DriverId")]
    public virtual User Driver { get; set; }
}

public class UserTrip
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TripId { get; set; }
    
    [ForeignKey("UserId")]
    public virtual  User User { get; set; }
    
    [ForeignKey("TripId")]
    public virtual  Trip Trip { get; set; }
}

public class Location
{
    [Key]
    public int Id { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
}


