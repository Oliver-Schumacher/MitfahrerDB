using Microsoft.EntityFrameworkCore;
using MitfahrerDB_Backend.Models;

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
        this.ChangeTracker.LazyLoadingEnabled = false;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Eager Loading Trip
        modelBuilder.Entity<Trip>().Navigation(t => t.LocationStart).AutoInclude();
        modelBuilder.Entity<Trip>().Navigation(t => t.LocationEnd).AutoInclude();
        modelBuilder.Entity<Trip>().Navigation(t => t.Driver).AutoInclude();
        
        //Eager Loading User
        modelBuilder.Entity<User>().Navigation(u => u.Gender).AutoInclude();

        //Eager Loading UserTrip
        modelBuilder.Entity<UserTrip>().Navigation(u => u.User).AutoInclude();
        modelBuilder.Entity<UserTrip>().Navigation(u => u.Trip).AutoInclude();

    }
}