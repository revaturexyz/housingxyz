
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Revature.Tenant.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsController : ControllerBase
    {
    //private readonly IDataAccess db;

    //public TenantsController(IDataAccess dataAccess)
    //{
    //  db = dataAccess;
    //}


    [HttpPost]
    [ProducesResponseType(typeof(Lib.Models.Tenant), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Lib.Models.Tenant), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody][Required] Lib.Models.Tenant request)
    {
      if (data.Any(d => d.Id == request.Id))
      {
        return Conflict($"data with id {request.Id} already exists");
      }

      data.Add(request);

      // Send this to the bus for the other services
      await _serviceBusSender.SendMessage(new MyPayload
      {
        Goals = request.Goals,
        Name = request.Name,
        Delete = false
      });

      return Ok(request);
    }

    // GET: api/Tenants
    [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Tenants/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Tenants
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Tenants/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
