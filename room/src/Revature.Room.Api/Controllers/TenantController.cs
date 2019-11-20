using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Revature.Room.Lib;
using System;
using System.Threading.Tasks;
using Serilog;

namespace Revature.Room.Api.Controllers
{
  [Route("api/rooms")]
  [ApiController]
  public class TenantController : ControllerBase
  {
    private readonly IRepository _repository;
    private readonly ILogger _logger;

    /// <summary>
    /// Controller in charge of communicating with the tenant service
    /// </summary>

    public TenantController(IRepository repository, ILogger logger)
    {
      _repository = repository ?? throw new NullReferenceException("Repository cannot be null." + nameof(repository));
      _logger = logger ?? throw new NullReferenceException("Logger cannot be null." + nameof(logger));
    }

    // GET: api/rooms?gender=g&endDate=e
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync(
          [FromQuery] string gender,
          [FromQuery] DateTime endDate
          )
    {
      _logger.Information("Getting vacant filtered rooms...");
      var result = await _repository.GetVacantFilteredRoomsByGenderandEndDateAsync(gender, endDate);
      _logger.Information("Filtered rooms fetched.");
      return Ok(result);
    }
  }
}
