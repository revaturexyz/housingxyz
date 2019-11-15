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
      Guid complexId,
      [FromQuery] string roomNumber,
      [FromQuery] int? numberOfBeds,
      [FromQuery] string roomType,
      [FromQuery] string gender,
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
  }
}
