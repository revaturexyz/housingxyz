using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Revature.Address.Api.Models;
using Revature.Address.Lib.BusinessLogic;
using Revature.Address.Lib.Interfaces;

namespace Revature.Address.Api.Controllers
{
  /// <summary>
  /// This controller handles http requests sent to the
  /// address service
  /// </summary>
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

    // GET: api/address
    [HttpGet]
    public async Task<ActionResult<bool>> IsAddressDuplicated(AddressModel model)
    {
      Lib.Address bModel = new Lib.Address
      {
        Id = model.Id,
        Street = model.Street,
        City = model.City,
        State = model.State,
        Country = model.Country,
        ZipCode = model.ZipCode
      };

      Lib.Address address = (await db.GetAddressAsync(address: bModel)).FirstOrDefault();

      if (address == null)
      {
        _logger.LogInformation("Address does not exist in the database");
        return false;
      }

      _logger.LogError("Address already exists in the database");
      return true;
    }

    // GET: api/address/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AddressModel>> GetAddressById(Guid id)
    {

      Lib.Address address = (await db.GetAddressAsync(id: id)).FirstOrDefault();

      if (address == null)
      {
        _logger.LogError("Address at {id} could not be found", id);
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

    // GET: api/address/getdistance
    [HttpGet("getdistance")]
    public async Task<ActionResult<bool>> IsInRange([FromQuery] List<AddressModel> addresses, [FromServices] AddressLogic addressLogic)
    {
      List<Lib.Address> checkAddresses = new List<Lib.Address>();
      foreach(AddressModel address in addresses)
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
        checkAddresses.Add(newAddress);
      }
        if (await addressLogic.IsInRangeAsync(checkAddresses[0], checkAddresses[1], 20))
        {
          _logger.LogInformation("These addresses are within range of each other");
          return true;
        } else
        {
          _logger.LogError("These addresses are not in range of each other");
          return false;
        }
    }

    // POST: api/address
    [HttpPost]
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
