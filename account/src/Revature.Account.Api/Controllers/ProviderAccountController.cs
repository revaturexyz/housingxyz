using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Revature.Account.Lib.Interface;
using Revature.Account.Lib.Model;
using System;
using System.Threading.Tasks;

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

    // GET: api/provider-accounts/5
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

    // POST: api/provider-accounts
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] ProviderAccount newProvider)
    {
      try
      {
        Lib.Model.ProviderAccount mappedProvider = new Lib.Model.ProviderAccount()
        {
          ProviderId = Guid.NewGuid(),
          Name = newProvider.Name,
          Status = await _repo.GetStatusByIdAsync(1),
          AccountCreatedAt = DateTime.Now,
          AccountExpiresAt = DateTime.Now.AddDays(7)
        };
        _repo.AddProviderAccountAsync(mappedProvider);
        await _repo.SaveAsync();
        return CreatedAtRoute("GetProviderAccountById", new { id = mappedProvider.ProviderId }, mappedProvider);
      }
      catch
      {
        return BadRequest();
      }
    }

    // PUT: api/provider-accounts/5
    [HttpPut("{providerId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(Guid providerId, [FromBody] ProviderAccount provider)
    {
      var existingProvider = await _repo.GetProviderAccountByIdAsync(providerId);
      if (existingProvider != null)
      {
        existingProvider.Name = provider.Name;
        existingProvider.AccountCreatedAt = DateTime.Now;
        await _repo.UpdateProviderAccountAsync(existingProvider);
        await _repo.SaveAsync();
        return NoContent();
      }
      return NotFound();
    }

    // DELETE: api/provider-accounts/5
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
