using System.Threading.Tasks.Dataflow;
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
        // var trip = _db.Trips.FirstOrDefault(t => t.Id == id);
        // if (trip is null) return BadRequest($"Trip {id} could not be found");
        // return Ok(trip);

        var tripsDriver = _db.Trips.Join(_db.Users, trip => trip.DriverId,
            user => user.Id,
            ((trip, user) => new
            {
                DriverId = user.Id,
                DriverName = user.Name
            })).ToList();
        return Ok(tripsDriver);
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
        var driverError = ValidateDriver(driverId);
        if (!driverError.success) return BadRequest(driverError.message);
        
        //TODO ValidateStartTime
        //TODO ValidateSeats

        var locationStart = GetLocation(locStartLat, locStartLong);
        var locationEnd = GetLocation(locEndLat, locEndLong);
        
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

    /// <summary>
    /// Tries to get a Location based on Latitude and Longitude, If not exists Create a new Database Table
    /// </summary>
    /// <param name="latitude"></param>
    /// <param name="longitude"></param>
    /// <returns></returns>
    [NonAction]
    private Location GetLocation(string latitude, string longitude)
    {
        var location = _db.Locations.FirstOrDefault(l => (l.Latitude == latitude) && (l.Longitude == longitude));
        if (location is not null) return location;
        location = new Location()
        {
            Latitude = latitude,
            Longitude = longitude
        };

        _db.Locations.Add(location);
        _db.SaveChanges();

        return location;
    }
    
    [NonAction]
    private (bool success, string message) ValidateDriver(int id)
    {
        if (id == 0) return (false, "DriverId 0 is invalid");
        
        var driver = _db.Users.FirstOrDefault(d => d.Id == id);
        if (driver is null) return (false, $"The Driver with the ID {id} does not exist");
        
        return (true, "");
    }

    [HttpPut]
    [Route("/Trip/{id}")]
    public IActionResult OnPut(string locStartLong, 
                                string locStartLat, 
                                string locEndLong, 
                                string locEndLat, 
                                int driverId, 
                                string startTime, 
                                bool sameGender,
                                int availableSeats,
                                int id)
    {
        
        var trip = _db.Trips.FirstOrDefault(t => t.Id == id);
        if (trip is null) return BadRequest($"The Trip {id} could not be found");
        
        var locationStart = GetLocation(locStartLat, locStartLong);
        var locationEnd = GetLocation(locEndLat, locEndLong);
        
        var driverError = ValidateDriver(driverId);
        if (!driverError.success) return BadRequest(driverError.message);
        
        return Ok();
    }



}