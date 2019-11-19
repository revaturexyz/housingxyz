using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Revature.Room.Lib;
using ServiceBusMessaging;

namespace Revature.Room.Api.Controllers
{
    [Route("api/rooms")]
    [ApiController]
    public class TenantController : ControllerBase
    {
    private readonly IServiceBusSender _busSender;
    private readonly IRepository _repository;

    /// <summary>
    /// Controller for the Rooms
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
