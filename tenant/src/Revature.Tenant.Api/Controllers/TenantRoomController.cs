using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Revature.Tenant.Lib.Interface;
using Revature.Tenant.Lib.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Revature.Tenant.Api.Controllers
{
  [Route("api/Tenant/")]
  [ApiController]
  public class TenantRoomController : ControllerBase
  {
    private readonly ITenantRoomRepository _repository;
    private readonly ILogger _logger;
    private readonly ITenantRepository _repo2;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _baseURI;

    public TenantRoomController(ITenantRoomRepository repository, ITenantRepository repo2, ILogger<TenantRoomController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
      _repository = repository;
      _repo2 = repo2;
      _logger = logger;
      _httpClientFactory = httpClientFactory;
      _baseURI = configuration["AppServices:Room"];
    }

    [HttpGet]
    [Route("Unassigned")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTenantsNotAssignedRoom()
    {
      _logger.LogInformation("Getting roomless tenants...");
      var tenants = await _repository.GetRoomlessTenantsAsync();
      _logger.LogInformation("Successfully gathered roomless tenants...");

      return Ok(tenants);
    }

    [HttpGet]
    [Route("Assign/AvailableRooms")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetTenantsByRoomId([FromQuery] string gender, [FromQuery] DateTime endDate)
    {
      _logger.LogInformation("Requesting room id + total beds from Room Service...");

      HttpClient client = _httpClientFactory.CreateClient();
      string resourceUri = "api/rooms?gender=" + gender + "&endDate=" + endDate;
      var response = await client.GetAsync(_baseURI + resourceUri);
      if (response.IsSuccessStatusCode)
      {
        var contentAsString = await response.Content.ReadAsStringAsync();
        var availableRooms = JsonSerializer.Deserialize<List<AvailRoom>>(contentAsString);

        var roomsWithTenants = new List<RoomInfo>();
        var getTenants = new List<Task<List<Lib.Models.Tenant>>>();

        _logger.LogInformation("Getting Tenants by Room Id...");

        foreach (var room in availableRooms)
        {
          roomsWithTenants.Add(
            new RoomInfo
            {
              RoomId = room.item1,
              NumberOfBeds = room.item2
            }
          );
          getTenants.Add(
           _repository.GetTenantsByRoomIdAsync(room.item1)
          );
        }

        var results = await Task.WhenAll(getTenants);
        int i = 0;
        foreach (var item in results)
        {
          roomsWithTenants[i].Tenants = item;
          i++;
        }

        _logger.LogInformation("Success.");

        return Ok(roomsWithTenants);
      }
      else
      {
        _logger.LogInformation("Could not retrieve info from Room Service.");
        return BadRequest();
      }
    }

    [HttpPut]
    [Route("Assign/{tenantId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AssignTenantToRoom(Guid tenantId, [FromQuery] Guid roomId)
    {
      try
      {
        _logger.LogInformation("Assigning tenant to room");
        var tenant = await _repo2.GetByIdAsync(tenantId);
        tenant.RoomId = roomId;
        return NoContent();
      }
      catch (ArgumentNullException ex)
      {
        _logger.LogInformation("Tenant cannot be found", ex);
        return NotFound();
      }
    }
  }
}
