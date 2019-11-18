using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Revature.Address.Api.Models;
using Revature.Address.Lib.Interfaces;
using Serilog;

namespace Revature.Address.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AddressController : ControllerBase
  {

    private readonly IDataAccess db;

    public AddressController(IDataAccess dataAccess)
    {
      db = dataAccess;
    }

    // GET: api/Users/5
    [Route("{id}")]
    [HttpGet("{id}", Name = "Get")]
    public async Task<ActionResult<AddressModel>> Get(Guid id)
    {
      Revature.Address.Lib.Address address = (await db.GetAddressesAsync(id: id)).FirstOrDefault();

      if (address == null)
      {
        Log.Error("Address Id: {id} could not be found", id);
        return NotFound();
      }

      Log.Information("Got Address");
      return Ok(new AddressModel
      {
        Id = address.Id,
        Street = address.Street,
        City = address.City,
        State = address.State,
        Country = address.Country,
        ZipCode = address.ZipCode
      });
    }

    [HttpPost]
    public async Task<ActionResult> PostUser([FromBody] AddressModel address)
    {
      Revature.Address.Lib.Address newAddress = new Revature.Address.Lib.Address
      {
        Id = address.Id,
        Street = address.Street,
        City = address.City,
        State = address.State,
        Country = address.Country,
        ZipCode = address.ZipCode
      };

      try
      {
        await db.AddAddressAsync(newAddress);
        await db.SaveAsync();
      }
      catch (InvalidOperationException ex)
      {
        return BadRequest($"{ex.Message}");
      }

      Log.Information("New user {name}");
      return CreatedAtRoute("Get", new { address.Id }, address);
    }

  }
}
