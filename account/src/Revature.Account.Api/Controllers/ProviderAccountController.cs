using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Revature.Account.Lib.Interface;
using Revature.Account.Lib.Model;

namespace Revature.Account.Api.Controllers
{
  [Route("api/provider-accounts")]
  [ApiController]
  public class ProviderAccountController : ControllerBase
  {
    private readonly IGenericRepository _repo;
    public ProviderAccountController(IGenericRepository repo)
    {
      _repo = repo ?? throw new ArgumentNullException(nameof(repo));
    }

    // GET: api/ProviderAccount/5
    [HttpGet("{providerId}", Name = "GetProviderAccountById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Get(Guid providerId)
    {
      var provider = await _repo.GetProviderAccountByIdAsync(providerId);
      if (provider == null)
      {
        return NotFound();
      }
      return Ok(provider);
    }

    // POST: api/ProviderAccount
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] ProviderAccount newProvider)
    {
      try
      {
        Lib.Model.ProviderAccount mappedProvider = new Lib.Model.ProviderAccount()
        {
          ProviderId = Guid.NewGuid(),
          Name = newProvider.Name,
          Password = newProvider.Password,
          Status = "Pending",
          AccountCreated = DateTime.Now,
          Expire = DateTime.Now.AddDays(7)
        };
        _repo.AddNewProviderAccountAsync(mappedProvider);
        await _repo.SaveAsync();
        return CreatedAtRoute($"api/provider-accounts/{mappedProvider.ProviderId}", new { id = mappedProvider.ProviderId }, mappedProvider);
      }
      catch
      {
        return BadRequest();
      }
    }

    // PUT: api/ProviderAccount/5
    [HttpPut("{providerId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(Guid providerId, [FromBody] ProviderAccount provider)
    {
      var existingProvider = await _repo.GetProviderAccountByIdAsync(providerId);
      if (existingProvider != null)
      {
        existingProvider.Name = provider.Name;
        existingProvider.Password = provider.Password;
        existingProvider.AccountCreated = DateTime.Now;
        await _repo.UpdateProviderAccountAsync(existingProvider);
        await _repo.SaveAsync();
        return NoContent();
      }
      return NotFound();
    }

    // DELETE: api/ApiWithActions/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid providerId)
    {
      var existingProvider = await _repo.GetProviderAccountByIdAsync(providerId);
      if (existingProvider != null)
      {
        await _repo.DeleteProviderAccountAsync(providerId);
        await _repo.SaveAsync();
        return NoContent();
      }
      return NotFound();
    }
  }
}
