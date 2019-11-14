using Microsoft.AspNetCore.Mvc;
using Revature.Room.Lib;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Revature.Room.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class RoomController : ControllerBase
  {
    /// <summary>
    /// Controller for the Room
    /// </summary>
    private readonly IRepository _repository;

    public RoomController(IRepository repository)
    {
      _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetRoom(int complexId, string roomNumber)
    {
      Lib.Room room = await _repository.GetRoom(complexId, roomNumber);
      return Ok(room);
    }

    [HttpGet]
    public async Task<IActionResult> GetRoomsOfComplex(int complexId)
    {
      IEnumerable<Lib.Room> rooms = await _repository.GetRoomsOfComplex(complexId);
      return Ok(rooms);
    }

    [HttpGet]
    public async Task<IActionResult> GetRoomsWithNumberOfBeds(int complexId, int numOfBeds)
    {
      IEnumerable<Lib.Room> rooms = await _repository.GetRoomsWithNumberOfBeds(complexId, numOfBeds);
      return Ok(rooms);
    }

    [HttpGet]
    public async Task<IActionResult> GetRoomsOfType(int complexId, RoomType roomType)
    {
      IEnumerable<Lib.Room> rooms = await _repository.GetRoomsWithType(complexId, roomType);
      return Ok(rooms);
    }

    [HttpGet]
    public async Task<IActionResult> GetRoomsOfGender(int complexId, Gender gender)
    {
      IEnumerable<Lib.Room> rooms = await _repository.GetRoomsOfGender(complexId, gender);
      return Ok(rooms);
    }

    [HttpGet]
    public async Task<IActionResult> GetRoomsBeforeLeaseEndDate(int complexId, DateTime endDate)
    {
      IEnumerable<Lib.Room> rooms = await _repository.GetRoomsBeforeLeaseEndDate(complexId, endDate);
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
