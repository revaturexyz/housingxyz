using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    private readonly IRepository _repository;
    private readonly ILogger _logger;
    private readonly IServiceBusSender _busSender;

    public ComplexController(IRepository repository, ILogger logger, IServiceBusSender sender)
    {
      _repository = repository;
      _logger = logger;
      _busSender = sender;
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
    /// <exception cref="KeyNotFoundException">ComplexId or RoomId was not found in the DB</exception>

    [HttpGet] // /complexes/{complexId}/rooms?roomNumber=a&numberOfBeds=b&roomType=c&gender=d&endDate=e&roomId=f
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFilteredRoomsAsync(
      Guid complexId,
      [FromQuery] string roomNumber,
      [FromQuery] int? numberOfBeds,
      [FromQuery] string roomType,
      [FromQuery] string gender,
      [FromQuery] DateTime? endDate,
      [FromQuery] Guid? roomId)
    {
      try
      {
        _logger.LogInformation("Getting filtered rooms...");
        IEnumerable<Lib.Room> rooms = await _repository.GetFilteredRoomsAsync(
        complexId,
        roomNumber,
        numberOfBeds,
        roomType,
        gender,
        endDate,
        roomId);
        _logger.LogInformation("Success.");
        return Ok(rooms);
      }
      catch (KeyNotFoundException ex)
      {
        _logger.LogError(ex.Message);
        return NotFound(ex);
      }
    }

    /// <summary>
    /// Reads/gets a room based on the provided room id: Guid
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    [HttpGet("{roomId}", Name = "GetRoom")]
    public async Task <Lib.Room> GetRoomAsync(Guid roomId)
    {
      return await _repository.ReadRoomAsync(roomId);
    }

    /// <summary>
    /// Creates a room based on the complex's needs
    /// </summary>
    /// <param name="room"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PostRoomAsync
      ([FromBody, Bind("ComplexID, RoomID, RoomNumber, NumberOfBeds, NumberOfOccupants, Gender, RoomType, LeaseStart, LeaseEnd")]Revature.Room.Lib.Room room)
    {
      Revature.Room.Lib.Room createdRoom = new Revature.Room.Lib.Room
      {
        ComplexId = room.ComplexId,
        RoomId = room.RoomId,
        RoomNumber = room.RoomNumber,
        NumberOfBeds = room.NumberOfBeds,
        NumberOfOccupants = room.NumberOfOccupants,
        Gender = room.Gender,
        RoomType = room.RoomType
      };
      createdRoom.SetLease(room.LeaseStart, room.LeaseEnd);
      await _repository.CreateRoomAsync(createdRoom);
      await _repository.SaveAsync();

      //await _busSender.SendCreateMessage(createdRoom);

      return CreatedAtRoute("GetRoom", new { RoomID = createdRoom.RoomId }, createdRoom);

    }

    /// <summary>
    /// Update a room given the provided their id: Guid
    /// </summary>
    /// <param name="id"></param>
    /// <param name="Lroom"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRoomAsync(Guid id, [FromBody] Revature.Room.Lib.Room Lroom)
    {
      Revature.Room.Lib.Room ro = new Revature.Room.Lib.Room();
      ro.RoomId = id;

      var IERooms = await _repository.ReadRoomAsync(ro.RoomId);

      Revature.Room.Lib.Room newRo = new Revature.Room.Lib.Room
      {
        Gender = Lroom.Gender,
        NumberOfOccupants = Lroom.NumberOfOccupants,
      };
      newRo.SetLease(Lroom.LeaseStart, Lroom.LeaseEnd);
      await _repository.UpdateRoomAsync(newRo);
      await _repository.SaveAsync();

      //await _busSender.SendUpdateMessage(newRo);

      return Ok(newRo);
    }

    /// <summary>
    /// Delete room based on Id: Guid
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("DeleteRoom")]
    public async Task<IActionResult> DeleteRoomAsync(Guid id)
    {
      Revature.Room.Lib.Room ro = new Revature.Room.Lib.Room();
      ro.RoomId = id;

      await _repository.DeleteRoomAsync(ro.RoomId);
      await _repository.SaveAsync();

     await _busSender.SendDeleteMessage(ro);

      return NoContent();
    }

    /// <summary>
    /// Deletes a complex and deletes all the rooms related to that specified complex id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("DeleteComplex")]
    public async Task<IActionResult> DeleteComplexAsync(Guid id)
    {
      Revature.Room.Lib.Room co = new Revature.Room.Lib.Room();
      co.ComplexId = id;

      var listOfGuid = await _repository.DeleteComplexRoomAsync(co.ComplexId);
      await _repository.SaveAsync();

      await _busSender.SendDeleteComplexMessage(listOfGuid);

      return NoContent();
    }
  }
}
