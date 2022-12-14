using System.Threading.Tasks.Dataflow;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MitfahrerDB_Backend.Models;
using MitfahrerDB_Backend.RequestBodys;

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
    public IActionResult Get()
    {
        var trips = _db.Trips.ToList();
        return Ok(trips);
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
    public ActionResult Post([FromBody]TripBody tripBody)
    {
        var driverError = ValidateDriver(tripBody.DriverId);
        if (!driverError.success) return BadRequest(driverError.message);
        
        //At a post the Coordinates are required!.
        if (tripBody.LocationStartLat == null || tripBody.LocationStartLat == String.Empty)
            return BadRequest("LocationStartLat is required!");
        if (tripBody.LocationStartLon == null || tripBody.LocationStartLon == String.Empty)
            return BadRequest("LocationStartLon is required!");
        if (tripBody.LocationEndLat == null || tripBody.LocationEndLat == String.Empty)
            return BadRequest("LocationEndLat is required!");
        if (tripBody.LocationEndLon == null || tripBody.LocationEndLon == String.Empty)
            return BadRequest("LocationEndLon is required!");

        var locationStart = GetLocation(tripBody.LocationStartLat, tripBody.LocationStartLon);
        var locationEnd = GetLocation(tripBody.LocationEndLat, tripBody.LocationEndLon);

        var trip = new Trip()
        {
            LocationStartId = locationStart.Id,
            LocationEndId = locationEnd.Id,
            DriverId = tripBody.DriverId,
            Address = tripBody.Address,
            Lesson = tripBody.Lesson,
            SameGender = tripBody.SameGender,
            WeekDay = tripBody.WeekDay,
            ToGSO = tripBody.ToGSO,
            AvailableSeats = tripBody.AvailableSeats
        };
        _db.Trips.Add(trip);
        _db.SaveChanges();
        
        return Ok(trip);
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
    public IActionResult OnPut([FromBody] TripBody tripBody, int id)
    {
        var trip = _db.Trips.FirstOrDefault(t => t.Id == id);
        if (trip is null) return BadRequest($"The Trip {id} could not be found");

        if ((tripBody.LocationEndLat != null && tripBody.LocationEndLat != String.Empty) && (tripBody.LocationEndLon != null && tripBody.LocationEndLon != String.Empty))
        {
            var locationEnd = GetLocation(tripBody.LocationEndLat, tripBody.LocationEndLon);
            trip.LocationEndId = locationEnd.Id;
        }
        if ((tripBody.LocationStartLat != null && tripBody.LocationStartLat != String.Empty) && (tripBody.LocationStartLon != null && tripBody.LocationStartLon != String.Empty))
        {
            var locationStart = GetLocation(tripBody.LocationStartLat, tripBody.LocationStartLon);
            trip.LocationStartId = locationStart.Id;
        }
        trip.Lesson = tripBody.Lesson;
        trip.SameGender = tripBody.SameGender;
        trip.AvailableSeats = tripBody.AvailableSeats;
        trip.Address = tripBody.Address;
        trip.WeekDay = tripBody.WeekDay;
        trip.ToGSO = tripBody.ToGSO;
        
        return (_db.SaveChanges() != 0 ? Ok(trip) : BadRequest($"Could not update trip {id}")); 
    }

    [Route("/Trip/{id}")]
    [HttpDelete]
    public IActionResult OnDelete(int id)
    {
        var trip = _db.Trips.FirstOrDefault(t => t.Id == id);
        if (trip is null) return BadRequest($"Could not find Trip {id}");

        _db.Trips.Remove(trip);
        return (_db.SaveChanges() != 0 ? Ok() : BadRequest($"Could not Delete Trip {id}"));
    }

}