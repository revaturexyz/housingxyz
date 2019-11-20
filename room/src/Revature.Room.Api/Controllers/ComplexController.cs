using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Revature.Room.Lib;
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

    public ComplexController(IRepository repository, ILogger logger)
    {
      _repository = repository;
      _logger = logger;
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
  }
}
