using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Revature.Room.Lib;
using ServiceBusMessaging;
using System;
using System.Threading.Tasks;

namespace Revature.Room.Api.Controllers
{
  [Route("api/rooms")]
  [ApiController]
  public class TenantController : ControllerBase
  {
    private readonly IServiceBusSender _busSender;
    private readonly IRepository _repository;

    /// <summary>
    /// Controller in charge of communicating with the tenant service
    /// </summary>

    public TenantController(IRepository repository, IServiceBusSender busSender)
    {
      _repository = repository;
      _busSender = busSender ?? throw new ArgumentNullException();
    }

    // GET: api/rooms?gender=g&endDate=e
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync(
          [FromQuery] string gender,
          [FromQuery] DateTime endDate
          )
    {
      return Ok(await _repository.GetVacantFilteredRoomsByGenderandEndDateAsync(gender, endDate));
    }
  }
}
