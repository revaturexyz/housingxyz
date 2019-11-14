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
  public class ProviderController : ControllerBase
  {
    private readonly IProviderRepository _providerRepository;

    public ProviderController(IProviderRepository providerRepository)
    {
      _providerRepository = providerRepository ??
        throw new ArgumentNullException(nameof(providerRepository), "Provider repo cannot be null");
    }

    /// <summary>
    /// Gets all providers
    /// </summary>
    /// <returns></returns>
    // GET: api/provider
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ApiProvider>>> GetAllAsync()
    {
      try
      {
        var providers = await _providerRepository.GetAllAsync();

        var apiProviders = providers.Select(pro => new ApiProvider
        {
          ProviderId = pro.ProviderId,
          CompanyName = pro.CompanyName,
          Username = pro.Username,
          ContactNumber = pro.ContactNumber,
          ApiAddress = pro.Address is null ? null : new ApiAddress
          {
            StreetAddress = pro.Address.StreetAddress,
            City = pro.Address.City,
            State = pro.Address.State,
            ZipCode = pro.Address.Zip
          },
          ApiTrainingCenter = pro.Center is null ? null : new ApiTrainingCenter
          {
            CenterId = pro.Center.CenterId,
            CenterName = pro.Center.CenterName,
            ContactNumber = pro.Center.ContactNumber,
            ApiAddress = ApiModelFactory.MakeApiAddress(pro.Center.Address),
          }
        });
        return Ok(apiProviders);
      }
      catch (Exception e)
      {
        return StatusCode(500, e.Message);
      }
    }

    /// <summary>
    /// Gets provider by provider ID
    /// </summary>
    /// <param name="providerId"></param>
    /// <returns></returns>
    // GET: api/provider/5
    [HttpGet("{providerId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiProvider>> GetAsync([FromRoute]int providerId)
    {
      try
      {
        var provider = await _providerRepository.GetAsync(providerId);
        var apiProvider = new ApiProvider
        {
          ProviderId = provider.ProviderId,
          CompanyName = provider.CompanyName,
          Username = provider.Username,
          Password = provider.Password,
          ContactNumber = provider.ContactNumber,
          ApiAddress = provider.Address is null ? null : new ApiAddress
          {
            AddressId = provider.Address.AddressId,
            StreetAddress = provider.Address.StreetAddress,
            City = provider.Address.City,
            State = provider.Address.State,
            ZipCode = provider.Address.Zip
          },

          ApiTrainingCenter = provider.Center is null ? null : new ApiTrainingCenter
          {
            CenterId = provider.Center.CenterId,
            CenterName = provider.Center.CenterName,
            ContactNumber = provider.Center.ContactNumber,
            ApiAddress = ApiModelFactory.MakeApiAddress(provider.Center.Address),
          }
        };
        return Ok(apiProvider);
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
    /// Updates a provider
    /// </summary>
    /// <param name="apiProvider"></param>
    /// <param name="providerId"></param>
    /// <returns></returns>
    // PUT: api/provider/5
    [HttpPut("{providerId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult> PutAsync([FromRoute]int providerId, [FromBody]ApiProvider apiProvider)
    {
      if (apiProvider.ProviderId != providerId)
      {
        return Conflict();
      }
      try
      {
        var provider = await _providerRepository.GetAsync(apiProvider.ProviderId);

        provider.Username = apiProvider.Username;
        provider.Password = apiProvider.Password;
        provider.ContactNumber = apiProvider.ContactNumber;
        provider.CompanyName = apiProvider.CompanyName;
        if (provider.Address != null && provider.Address.AddressId != apiProvider.ApiAddress.AddressId)
        {
          provider.Address.AddressId = apiProvider.ApiAddress.AddressId;
          provider.Address.StreetAddress = apiProvider.ApiAddress.StreetAddress;
          provider.Address.City = apiProvider.ApiAddress.City;
          provider.Address.State = apiProvider.ApiAddress.State;
          provider.Address.Zip = apiProvider.ApiAddress.ZipCode;
        }
        await _providerRepository.UpdateAsync(provider, providerId);
        return NoContent();
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
