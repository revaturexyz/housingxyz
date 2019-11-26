using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Revature.Tenant.Lib.Interface;
using Revature.Tenant.Lib.Models;

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
    public async Task<IActionResult> GetTenantsNotAssignedRoom()
    {
      _logger.LogInformation("Getting roomless tenants...");
      var tenants = await _repository.GetRoomlessTenants();
      _logger.LogInformation("Successfully gathered roomless tenants...");

      return Ok(tenants);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTenantsByRoomId([FromQuery] string gender, [FromQuery] DateTime endDate)
    {
      try
      {
        _logger.LogInformation("Requesting room id + total beds fmor Room Service...");
        using (var client = new HttpClient())
        {
          string baseUri = "baseUriForRoomService.com/";
          string resourceUri = "api/rooms?gender=" + gender + "&endDate=" + endDate;
          var response = await client.GetAsync(baseUri+resourceUri);
          if (response.IsSuccessStatusCode)
          {
            var contentAsString = await response.Content.ReadAsStringAsync();
            var availableRooms = JsonSerializer.Deserialize<List<Tuple<Guid,int>>>(contentAsString);

            var roomsWithTenants = new List<RoomInfo>();

            _logger.LogInformation("Getting Tenants by Room Id...");

            foreach (var room in availableRooms)
            {
              roomsWithTenants.Add(
                new RoomInfo
                {
                  RoomId = room.Item1,
                  NumberOfBeds = room.Item2,
                  Tenants = await _repository.GetTenantsByRoomId(room.Item1) as List<Lib.Models.Tenant>
                }
              );
            }

            _logger.LogInformation("Success.");


            return Ok(roomsWithTenants);
          } else
          {
            _logger.LogInformation("Could not retrieve info from Room Service.");
            return BadRequest();
          }
        }
      }
      catch (ArgumentNullException ex)
      {
        _logger.LogError(ex, "Tried to parse a null tenant(?)");

        return NotFound(ex);
      }
    }
  }
}
