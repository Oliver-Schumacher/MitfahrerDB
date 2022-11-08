using Microsoft.AspNetCore.Mvc;

namespace MitfahrerDB_Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class TripController : ControllerBase
{
    private readonly ILogger<TripController> _logger;
    private readonly DataBaseContext _db = new DataBaseContext();

    public TripController(ILogger<TripController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("/Trips")]
    public List<Trip> Get()
    {
        return _db.Trips.ToList();
    }

    [HttpGet]
    [Route("/Trip/{id}")]
    public ActionResult Get(int id)
    {
        var trip = _db.Trips.FirstOrDefault(t => t.Id == id);
        if (trip is null) return BadRequest($"Trip {id} could not be found");
        return Ok(trip);
    }

    [HttpPost]
    public ActionResult Post(string locStartLong, 
                            string locStartLat, 
                            string locEndLong, 
                            string locEndLat, 
                            int driverId, 
                            string startTime, 
                            bool sameGender,
                            int availableSeats)
    {
        var locationStart = new Location()
        {
            Latitude = locStartLat,
            Longitude = locStartLong
        };
        var locationEnd = new Location()
        {
            Latitude = locEndLat,
            Longitude = locEndLong
        };

        var driverError = ValidateDriver(driverId);
        if (!driverError.success) return BadRequest(driverError.message);
        
        //TODO ValidateStartTime
        //TODO ValidateSeats

        _db.Locations.Add(locationStart);
        _db.Locations.Add(locationEnd);
        _db.SaveChanges();

        var trip = new Trip()
        {
            LocationStartId = locationStart.Id,
            LocationEndId = locationEnd.Id,
            DriverId = driverId,
            StartTime = startTime,
            SameGender = sameGender,
            AvailableSeats = availableSeats
        };
        _db.Trips.Add(trip);
        _db.SaveChanges();
        
        return Ok();
    }

    [NonAction]
    private (bool success, string message) ValidateDriver(int id)
    {
        if (id == 0) return (false, "DriverId 0 is invalid");
        
        var driver = _db.Users.FirstOrDefault(d => d.Id == id);
        if (driver is null) return (false, $"The Driver with the ID {id} does not exist");
        
        return (true, "");
    }

}