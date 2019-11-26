using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Revature.Tenant.Lib.Interface;

namespace Revature.Tenant.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TenantRoomController : ControllerBase
  {
    private readonly ITenantRoomRepository _repository;
    private readonly ILogger _logger;

    public TenantRoomController(ITenantRoomRepository repository, ILogger<TenantRoomController> logger)
    {
      _repository = repository;
      _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTenantsByRoomId([FromQuery] Guid roomId)
    {
      try
      {
        _logger.LogInformation("Getting Tenants by Room Id...");

        IEnumerable<Lib.Models.Tenant> tenants = await _repository.GetTenantsByRoomId(roomId);

        _logger.LogInformation("Success.");

        return Ok(tenants);
      }
      catch(KeyNotFoundException ex)
      {
        _logger.LogError(ex.Message);

        return NotFound(ex);
      }
    }
  }
}
