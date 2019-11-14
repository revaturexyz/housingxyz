using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xyz.Provider.Api.Models;
using Xyz.Provider.Lib.Interface;

namespace Xyz.Provider.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AddressController : ControllerBase
  {

    private readonly IAddressRepository _addressRepository;

    public AddressController(IAddressRepository addressRepository)
    {
      _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository), "Address repo cannot be null");
    }

    /// <summary>
    /// Get all addresses by complex ID
    /// </summary>
    /// <param name="complexId"></param>
    /// <returns></returns>
    // GET: api/address/complex/5
    [HttpGet("complex/{complexId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ApiAddress>>> GetByComplexIdAsync([FromRoute]int complexId)
    {
      try
      {
        var addi = await _addressRepository.GetAddressesByComplexIdAsync(complexId);

        var apiAddi = addi.Select(a => new ApiAddress
        {
          AddressId = a.AddressId,
          StreetAddress = a.StreetAddress,
          City = a.City,
          State = a.State,
          ZipCode = a.Zip
        });
        return Ok(apiAddi);
      }
      catch (ArgumentException)
      {
        return NotFound();
      }
      catch (Exception e)
      {
        return StatusCode(500, e.Message);
      }
    }

    /// <summary>
    /// Get all addresses by provider ID
    /// </summary>
    /// <param name="providerId"></param>
    /// <returns></returns>
    // GET: api/address/provider/5
    [HttpGet("provider/{providerId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ApiAddress>>> GetByProviderIdAsync([FromRoute]int providerId)
    {
      try
      {
        var addi = await _addressRepository.GetAddressesByProviderIdAsync(providerId);

        if (addi is null)
        {
          return NotFound();
        }
        else
        {
          var apiAddi = addi.Select(a => new ApiAddress
          {
            AddressId = a.AddressId,
            StreetAddress = a.StreetAddress,
            City = a.City,
            State = a.State,
            ZipCode = a.Zip
          });

          return Ok(apiAddi);
        }
      }
      catch (ArgumentException)
      {
        return NotFound();
      }
      catch (Exception e)
      {
        return StatusCode(500, e.Message);
      }
    }
  }
}
