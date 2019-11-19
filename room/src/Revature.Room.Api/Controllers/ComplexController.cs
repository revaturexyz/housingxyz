using Microsoft.AspNetCore.Mvc;
using Revature.Room.Lib;
using ServiceBusMessaging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Revature.Room.Api.Controllers
{
  /// <summary>
  /// Controller for commmunicating with the complex service
  /// </summary>
  [Route("api/complexes/{complexId}/rooms")]
  [ApiController]
  public class ComplexController : ControllerBase
  {
    private readonly IServiceBusSender _busSender;
    private readonly IRepository _repository;

    /// <summary>
    /// Controller for the Rooms
    /// </summary>

    public ComplexController(IRepository repository, IServiceBusSender busSender)
    {
      _repository = repository;
      _busSender = busSender ?? throw new ArgumentNullException();
    }

    /// <summary>
    /// This controller method is to get rooms based on filters applied (roomNumber, numberOfBeds, etc)
    /// </summary>
    /// <param name="complexId"></param>
    /// <param name="roomNumber"></param>
    /// <param name="numberOfBeds"></param>
    /// <param name="roomType"></param>
    /// <param name="gender"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>

    [HttpGet] // /complexes/{complexId}/rooms?roomNumber=a&numberOfBeds=b&roomType=c&gender=d&endDate=e
    public async Task<IActionResult> GetFilteredRoomsAsync(
      Guid complexId,
      [FromQuery] string roomNumber,
      [FromQuery] int? numberOfBeds,
      [FromQuery] string roomType,
      [FromQuery] string gender,
      [FromQuery] DateTime? endDate)
    {
      IEnumerable<Lib.Room> rooms = await _repository.GetFilteredRoomsAsync(
        complexId,
        roomNumber,
        numberOfBeds,
        roomType,
        gender,
        endDate);
      //Add a _busSender here

      return Ok(rooms);
    }

    /// <summary>
    /// Controller method for getting a specific room using room id
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>

    //Other HTTP Get goes here

    [HttpGet("{roomId}", Name = "GetRoom")]
    public async Task<IEnumerable<Lib.Room>> GetRoomAsync(Guid roomId)
    {
      if (roomId == null)
      {
        IEnumerable<Lib.Room> roomListNull = await _repository.ReadRoomAsync(roomId);

        await _busSender.SendReadMessage(roomListNull);

        return roomListNull;
      }
      IEnumerable<Lib.Room> roomListNotNull = await _repository.ReadRoomAsync(roomId);

      await _busSender.SendReadMessage(roomListNotNull);

      return roomListNotNull;
    }

    /// <summary>
    /// Controller method for adding a room
    /// </summary>
    /// <param name="room"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PostRoomAsync
      ([FromBody, Bind("ComplexId, RoomId, RoomNumber, NumberOfBeds, NumberOfOccupants, Gender, RoomType, LeaseStart, LeaseEnd")]Revature.Room.Lib.Room room)
    {
      Revature.Room.Lib.Room createdRoom = new Revature.Room.Lib.Room
      {
        ComplexId = room.ComplexId,
        RoomId = room.RoomId,
        RoomNumber = room.RoomNumber,
        NumberOfBeds = room.NumberOfBeds,
        NumberOfOccupants = room.NumberOfOccupants,
        Gender = room.Gender,
        RoomType = room.RoomType,
        LeaseStart = room.LeaseStart,
        LeaseEnd = room.LeaseEnd
      };
      await _repository.CreateRoomAsync(createdRoom);
      await _repository.SaveAsync();

      await _busSender.SendCreateMessage(createdRoom);

      //Will change route http after we have a HTTP GET

      return CreatedAtRoute("GetRoom", new { RoomId = createdRoom.RoomId }, createdRoom);
    }

    /// <summary>
    /// Controller method for updating a room
    /// </summary>
    /// <param name="id"></param>
    /// <param name="Lroom"></param>
    /// <returns></returns>

    [HttpPut("{id}")]
    public async Task<IActionResult> PutRoomAsync(Guid id, [FromBody] Revature.Room.Lib.Room Lroom)
    {
      Revature.Room.Lib.Room ro = new Revature.Room.Lib.Room();
      ro.RoomId = id;

      IEnumerable<Revature.Room.Lib.Room> IERooms = await _repository.ReadRoomAsync(ro.RoomId);

      Revature.Room.Lib.Room newRo = new Revature.Room.Lib.Room
      {
        Gender = Lroom.Gender,
        NumberOfOccupants = Lroom.NumberOfOccupants,
        LeaseStart = Lroom.LeaseStart,
        LeaseEnd = Lroom.LeaseEnd
      };

      await _repository.UpdateRoomAsync(newRo);
      await _repository.SaveAsync();

      await _busSender.SendUpdateMessage(newRo);

      return Ok(newRo);
    }

    /// <summary>
    /// Controller method for deleting a room
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    [HttpDelete]
    public async Task<IActionResult> DeleteRoomAsync(Guid id)
    {
      Revature.Room.Lib.Room ro = new Revature.Room.Lib.Room();
      ro.RoomId = id;

      await _repository.DeleteRoomAsync(ro.RoomId);
      await _repository.SaveAsync();

      await _busSender.SendDeleteMessage(ro);

      return NoContent();
    }
  }
}
