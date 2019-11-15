using System;
using System.Collections.Generic;
using System.Linq;
using BL = Revature.Room.Lib;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using ServiceBusMessaging;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Revature.Room.Api.Controllers
{
  [Route("api/[controller]")]
  public class ValuesController : Controller
  {
    private readonly ServiceBusSender _serviceBusSender;

    public ValuesController(ServiceBusSender servicebus)
    {
      _serviceBusSender = servicebus ?? throw new ArgumentNullException();
    }
    // GET: api/<controller>
    [HttpGet]
    public IEnumerable<string> Get()
    {
      return new string[] { "Service Bus", "Stuff" };
    }
        
    // GET api/<controller>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "value";
    }

    // POST api/<controller>
    [HttpPost]
    [ProducesResponseType(typeof(BL.Room), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BL.Room), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody][Required] BL.Room request)
    {
      // Checks for existing data
      //if (data.Any(d => d.Id == request.Id))
      //{
      //  return Conflict($"data with id {request.Id} already exists");
      //}

      //data.Add(request);

      // Send this to the bus for the other services
      await _serviceBusSender.SendMessage(new BL.Room
      {
        RoomID = request.RoomID,
        RoomNumber = request.RoomNumber,
      });

      return Ok(request);
    }

    // PUT api/<controller>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/<controller>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
