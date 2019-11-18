using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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
    [HttpGet]
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
    [Filters.GoogleAddressValidator]
    public async Task<ActionResult> PostUser([FromQuery] AddressModel address)
    {
      //if (ModelState.IsValid)
      //{

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
          return Ok("Address uccessfuly created");
        }
        catch (InvalidOperationException ex)
        {
          return BadRequest($"{ex.Message}");
        }
      //} else
      //{
      //  return BadRequest("Invalid Address");
      //}
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(
            [FromRoute] Guid id)
    {
      var item = await db.GetAddressesAsync(id);
      if (item is null)
      {
        return NotFound();
      }

      await db.DeleteAddressAsync(id);
      return NoContent();
    }

  }
}
