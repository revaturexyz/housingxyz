using Microsoft.AspNetCore.Mvc;
using Revature.Room.Lib;
using ServiceBusMessaging;
using System;
using System.Collections.Generic;
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
    public async Task<IList<Guid>> GetAsync(
          [FromQuery] string gender,
          [FromQuery] DateTime endDate
          )
    {
      return await _repository.GetVacantFilteredRoomsByGenderandEndDateAsync(gender, endDate);
    }
  }
}
