using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Revature.Address.Api.Models;
using Revature.Address.Lib.BusinessLogic;
using Revature.Address.Lib.Interfaces;

namespace Revature.Address.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {

    private readonly IDataAccess db;
    private readonly ILogger _logger;

    public AddressController(IDataAccess dataAccess, ILogger<AddressController> logger = null)
    {
      db = dataAccess;
      _logger = logger;
    }

    // GET: api/address/guid
    [Route("{id}", Name = "GetAddress")]
    [HttpGet]
    public async Task<ActionResult<AddressModel>> GetAddressById(Guid id)
    {
      Lib.Address address = (await db.GetAddressAsync(id: id)).FirstOrDefault();

      if (address == null)
      {
        _logger.LogError("Address Id: {id} could not be found", id);
        return NotFound();
      }

      _logger.LogInformation("Got Address");
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

    // POST: api/address/tenant
    [HttpPost]
    [Route("tenant")]
    public async Task<ActionResult> PostTenantAddress([FromBody] AddressModel address, [FromServices] AddressLogic addressLogic)
    {
        Lib.Address newAddress = new Lib.Address
        {
          Id = address.Id,
          Street = address.Street,
          City = address.City,
          State = address.State,
          Country = address.Country,
          ZipCode = address.ZipCode
        };


      if (await addressLogic.IsValidAddress(newAddress))
      {
        try
        {
          var newModel = new AddressModel
          {
            Id = newAddress.Id,
            Street = newAddress.Street,
            City = newAddress.City,
            State = newAddress.State,
            Country = newAddress.Country,
            ZipCode = newAddress.ZipCode
          };

          await db.AddAddressAsync(newAddress);
          await db.SaveAsync();
          _logger.LogInformation("Address successfully created");
          return CreatedAtRoute("GetAddress", new { newModel.Id}, newModel);
        }
        catch (InvalidOperationException ex)
        {
          _logger.LogError("That address is invalid");
          return BadRequest($"{ex.Message}");
        }
      } else
      {
        _logger.LogError("Address does not exist");
        return BadRequest("Invalid Address");
      }
    }

    [HttpPost]
    [Route("complex")]
    public async Task<ActionResult> PostComplexAddress([FromBody] List<AddressModel> addresses)
    {
      return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(
            [FromRoute] Guid id)
    {
      var item = await db.GetAddressAsync(id);
      if (item is null)
      {
        _logger.LogError("No address found matching given id");
        return NotFound();
      }

      await db.DeleteAddressAsync(id);
      _logger.LogInformation("Address successfully deleted");
      return NoContent();
    }

  }
}
