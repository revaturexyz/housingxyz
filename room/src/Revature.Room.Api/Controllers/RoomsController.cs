using Microsoft.AspNetCore.Mvc;
using Revature.Room.Lib;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Revature.Room.Api.Controllers
{
  [Route("api/complexes/{complexId}/rooms")]
  [ApiController]
  public class RoomsController : ControllerBase
  {
    /// <summary>
    /// Controller for the Rooms
    /// </summary>
    private readonly IRepository _repository;

    public RoomsController(IRepository repository)
    {
      _repository = repository;
    }

    [HttpGet] // /complexes/{complexId}/rooms?roomNumber=a&numberOfBeds=b&roomType=c&gender=d&endDate=e
    public async Task<IActionResult> GetFilteredRooms(
      int complexId,
      [FromQuery] string roomNumber,
      [FromQuery] int? numberOfBeds,
      [FromQuery] RoomType? roomType,
      [FromQuery] Gender? gender,
      [FromQuery] DateTime? endDate)
    {
      IEnumerable<Lib.Room> rooms = await _repository.GetFilteredRooms(
        complexId,
        roomNumber,
        numberOfBeds,
        roomType,
        gender,
        endDate);
      return Ok(rooms);
    }

    //[HttpGet]
    //public IActionResult getVacantRooms()
    //{
    //  // Initialize vacant rooms list For each room, get room id For each room id, query associate
    //  // server for count of associates with that room id If the number of associates is less than
    //  // the room.NumberOfBeds Add the room to the list of vacant rooms Return vacant rooms list
    //}

    //[HttpPost]
    //public void assignBed(Associate associate, Room room)
    //{
    //  // Check if associates of room is less than room.capacity Return the room is full! Check
    //  // associate.gender == room.gender Return associate gender is incorrect! Check
    //  // associate.batch.technology is the same as the technologies of associates of the room Return
    //  // associate technology does not match! Check that if associate.car and the cars of the room
    //  // is 2, then fail Return the room already has 2 cars! Check if room.leaseEnd is less than
    //  // associate.batch.endDate Return the lease ends before the batch ends! Return successfully
    //  // assigned room 201 CreatedAt
    //}
  }
}
