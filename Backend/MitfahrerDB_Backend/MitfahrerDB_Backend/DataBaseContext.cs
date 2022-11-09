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
        modelBuilder.Entity<Trip>()
            .HasOne<Location>(t => t.LocationStart)
            .WithMany();
        modelBuilder.Entity<Trip>()
            .HasOne<Location>(t => t.LocationEnd)
            .WithMany();
    }
}