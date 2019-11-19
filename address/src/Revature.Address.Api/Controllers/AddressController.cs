using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GoogleApi;
using GoogleApi.Entities;
using GoogleApi.Entities.Interfaces;
using GoogleApi.Entities.Maps.Geocoding;
using GoogleApi.Entities.Maps.Geocoding.Address.Request;
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
    [Route("{id}", Name = "GetAddress")]
    [HttpGet]
    public async Task<ActionResult<AddressModel>> GetAddressById(Guid id)
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
      AddressGeocodeRequest request = new AddressGeocodeRequest();
      request.Address = $"{newAddress.Street} {newAddress.City}, {newAddress.State} {newAddress.ZipCode} {newAddress.Country}";
      request.Key = SecretKey.GApiKey;
      GeocodeResponse response = await GoogleMaps.AddressGeocode.QueryAsync(request);
      var results = response.Results.ToArray();

      if (results.Length != 0)
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
          return CreatedAtRoute("GetAddress", new { newModel.Id}, newModel);
        }
        catch (InvalidOperationException ex)
        {
          return BadRequest($"{ex.Message}");
        }
      } else
      {
        return BadRequest("Invalid Address");
      }
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
