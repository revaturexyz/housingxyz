using Microsoft.AspNetCore.Mvc;
using Revature.Room.Lib;
using ServiceBusMessaging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using System.Linq;

namespace Revature.Room.Api.Controllers
{
  [Route("api/complexes/{complexId}/rooms")]
  [ApiController]
  public class RoomsController : ControllerBase
  {
    private readonly IServiceBusSender _busSender;
    private readonly IRepository _repository;

    /// <summary>
    /// Controller for the Rooms
    /// </summary>

    public RoomsController(IRepository repository, IServiceBusSender busSender)
    {
      _repository = repository;
      _busSender = busSender ?? throw new ArgumentNullException();
    }

    [HttpGet] // /complexes/{complexId}/rooms?roomNumber=a&numberOfBeds=b&roomType=c&gender=d&endDate=e
    //This controller method is to get rooms based on filters applied (roomNumber, numberOfBeds, etc)
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

    //Other HTTP Get goes here

    [HttpPost]
    public async Task<IActionResult> PostRoomAsync
      ([FromBody, Bind("ComplexID, RoomID, RoomNumber, NumberOfBeds, Gender, RoomType, LeaseStart, LeaseEnd")]Revature.Room.Lib.Room room)
    {
      Revature.Room.Lib.Room createdRoom = new Revature.Room.Lib.Room {
        ComplexID = room.ComplexID,
        RoomID = room.RoomID,
        RoomNumber = room.RoomNumber,
        NumberOfBeds = room.NumberOfBeds,
        Gender = room.Gender,
        RoomType = room.RoomType,
        LeaseStart = room.LeaseStart,
        LeaseEnd = room.LeaseEnd
      };
      await _repository.CreateRoomAsync(createdRoom);
      await _repository.SaveAsync();

      await _busSender.SendCreateMessage(createdRoom);

      //Will change route http after we have a HTTP GET

      return CreatedAtRoute("api/complexes/{complexId}/rooms", new { RoomID = createdRoom.RoomID }, createdRoom);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutRoomAsync(Guid id, [FromBody] Revature.Room.Lib.Room Lroom)
    {
      Revature.Room.Lib.Room ro = new Revature.Room.Lib.Room();
      ro.RoomID = id;

      IEnumerable<Revature.Room.Lib.Room> IERooms = await _repository.ReadRoomAsync(ro.RoomID);

      Revature.Room.Lib.Room newRo = new Revature.Room.Lib.Room
      {
        Gender = Lroom.Gender,
        LeaseStart = Lroom.LeaseStart,
        LeaseEnd = Lroom.LeaseEnd
      };

      await _repository.UpdateRoomAsync(newRo);
      await _repository.SaveAsync();

      await _busSender.SendUpdateMessage(newRo);

      return Ok(newRo);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteRoomAsync(Guid id)
    {
      Revature.Room.Lib.Room ro = new Revature.Room.Lib.Room();
      ro.RoomID = id;

      await _repository.DeleteRoomAsync(ro.RoomID);
      await _repository.SaveAsync();

      await _busSender.SendDeleteMessage(ro);

      return Ok(ro);
    }
  }
}
