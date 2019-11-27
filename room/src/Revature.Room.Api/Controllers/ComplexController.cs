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

    public ComplexController(IRepository repository, ILogger<ComplexController> logger, IServiceBusSender sender)
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
        _logger.LogError("Either complex Id or room Id was not in the DB", ex);
        return NotFound(ex);
      }
    }

    /// <summary>
    /// Reads/gets a room based given a roomId
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    [HttpGet("{roomId}", Name = "GetRoom")]
    [ProducesResponseType(typeof(Lib.Room), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRoomAsync(Guid roomId)
    {
      try
      {
        _logger.LogInformation("Getting room ready...");

        var result = await _repository.ReadRoomAsync(roomId);

        _logger.LogInformation("Success");

        return Ok(result);
      }
      catch (InvalidOperationException ex)
      {
        _logger.LogError("Room was not found in DB", ex);
        return NotFound();
      }
    }

    /// <summary>
    /// Creates a room based on the complex's needs
    /// </summary>
    /// <param name="room"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(Lib.Room), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostRoomAsync
      ([FromBody, Bind("ComplexID, RoomID, RoomNumber, NumberOfBeds, NumberOfOccupants, Gender, RoomType, LeaseStart, LeaseEnd")]Lib.Room room)
    {
      try
      {
        _logger.LogInformation("Adding a room");

        Lib.Room createdRoom = new Lib.Room
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

        _logger.LogInformation("Success. Room has been added");

        return CreatedAtRoute("GetRoom", new { RoomID = createdRoom.RoomId }, createdRoom);
      }
      catch (ArgumentException ex)
      {
        _logger.LogInformation("Lease was invalid", ex);
        return BadRequest();
      }
    }

    /// <summary>
    /// Update room's lease
    /// </summary>
    /// <param name="roomId"></param>
    /// <param name="room"></param>
    /// <returns></returns>
    /// <remarks>Update room functionality of complex service</remarks>
    [HttpPut("{roomId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutRoomAsync(Guid roomId, [FromBody] Lib.Room room)
    {
      try
      {
        _logger.LogInformation("Updating a room");

        var roomFromDb = await _repository.ReadRoomAsync(roomId);
        roomFromDb.SetLease(room.LeaseStart, room.LeaseEnd);

        await _repository.UpdateRoomAsync(roomFromDb);
        await _repository.SaveAsync();

        _logger.LogInformation("Success. Room has been updated");

        return NoContent();
      }
      catch (InvalidOperationException ex)
      {
        _logger.LogError("Room to update was not found in DB", ex);
        return NotFound();
      }
      catch (ArgumentException ex)
      {
        _logger.LogError("Lease is invalid", ex);
        return BadRequest();
      }
    }

    /// <summary>
    /// Delete room based on room Id
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    [HttpDelete("DeleteRoom")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRoomAsync(Guid roomId)
    {
      try
      {
        _logger.LogInformation("Deleting room");

        await _repository.DeleteRoomAsync(roomId);
        await _repository.SaveAsync();

        _logger.LogInformation("Success. Room has been deleted");

        _logger.LogInformation("Alerting complex service that room has been deleted...");

        await _busSender.SendDeleteMessage(new List<Guid>() { roomId });

        _logger.LogInformation("Success! Message has been sent!");

        return NoContent();
      }
      catch (InvalidOperationException ex)
      {
        _logger.LogError("Room to delete was not found in DB", ex);
        return NotFound();
      }
    }

    /// <summary>
    /// Deletes a complex and deletes all the rooms related to that specified complex roomId
    /// </summary>
    /// <param name="complexId"></param>
    /// <returns></returns>
    [HttpDelete("DeleteComplex")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteComplexAsync(Guid complexId)
    {
      try
      {
        _logger.LogInformation("Deleting rooms from complex");

        var listOfDeletedRoomIds = await _repository.DeleteComplexRoomAsync(complexId);
        await _repository.SaveAsync();

        _logger.LogInformation("Success! Rooms have been deleted");

        _logger.LogInformation("Sending deleted room roomId's to complex service");

        await _busSender.SendDeleteMessage(listOfDeletedRoomIds);

        _logger.LogInformation("Deleted room roomId's have been sent to complex service");

        return NoContent();
      }
      catch (InvalidOperationException ex)
      {
        _logger.LogError("Room to delete was not found in DB", ex);
        return NotFound();
      }
    }
  }
}
