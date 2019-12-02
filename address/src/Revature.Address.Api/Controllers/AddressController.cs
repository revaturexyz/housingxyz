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
    private readonly IAddressLogic _addressLogic;

    public AddressController(IDataAccess dataAccess, IAddressLogic addressLogic, ILogger<AddressController> logger = null)
    {
      db = dataAccess;
      _addressLogic = addressLogic;
      _logger = logger;
    }

    /// <summary>
    /// This method returns an address matching the given addressId
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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

    /// <summary>
    /// This method takes in two addressses from a url
    /// query string and returns true if they are within
    /// 20 miles of each other using Google's Distance MAtrix API
    /// and returns false if they are not.
    /// </summary>
    /// <param name="addresses"></param>
    /// <param name="addressLogic"></param>
    /// <returns></returns>
    // GET: api/address/checkdistance
    [HttpGet("checkdistance")]
    public async Task<ActionResult<bool>> IsInRange([FromQuery] AddressModel origin, [FromQuery] AddressModel destination)
    {
      Lib.Address start = new Lib.Address
      {
        Id = origin.Id,
        Street = origin.Street,
        City = origin.City,
        State = origin.State,
        Country = origin.Country,
        ZipCode = origin.ZipCode
      };
      Lib.Address end = new Lib.Address
      {
        Id = destination.Id,
        Street = destination.Street,
        City = destination.City,
        State = destination.State,
        Country = destination.Country,
        ZipCode = destination.ZipCode
      };
      if (await _addressLogic.IsInRangeAsync(start, end, 20))
      {
        _logger.LogInformation("These addresses are within range of each other");
        return true;
      }
      else
      {
        _logger.LogError("These addresses are not in range of each other");
        return false;
      }
    }

    /// <summary>
    /// This method checks if an address already exists in the database,
    /// and if it does, it returns its addressId. If it doesn't exist it checks if the address
    /// exists with Google's Geocode API and if it does it's added to the database and
    /// its addressId is returned, otherwise a bad request message is returned.
    /// </summary>
    /// <param name="address"></param>
    /// <param name="addressLogic"></param>
    /// <returns></returns>
    // GET: api/address
    [HttpGet]
    public async Task<ActionResult<Lib.Address>> GetAddress([FromQuery] AddressModel address)
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
      Lib.Address checkAddress = (await db.GetAddressAsync(address: newAddress)).FirstOrDefault();

      if (checkAddress == null)
      {
        _logger.LogInformation("Address does not exist in the database");
        newAddress.Id = Guid.NewGuid();
        if (await _addressLogic.IsValidAddressAsync(newAddress))
        {
          try
          {
            var normalAddress = await _addressLogic.NormalizeAddressAsync(newAddress);

            await db.AddAddressAsync(normalAddress);
            await db.SaveAsync();
            _logger.LogInformation("Address successfully created");
            return newAddress;
          }
          catch (Exception ex)
          {
            _logger.LogError("{0}", ex);
            return BadRequest($"Address entry failed");
          }
        }
        else
        {
          _logger.LogError("Address does not exist");
          return BadRequest("Address does not exist");
        }
      }
      else
      {

        _logger.LogError("Address already exists in the database");
        return checkAddress;
      }
    }
  }
}
