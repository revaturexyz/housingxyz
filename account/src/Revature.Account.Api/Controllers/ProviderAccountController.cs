using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Revature.Account.Lib.Interface;
using Revature.Account.Lib.Model;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Revature.Account.Api.Controllers
{
  [Route("api/provider-accounts")]
  [ApiController]
  public class ProviderAccountController : ControllerBase
  {
    private readonly IGenericRepository _repo;
    private readonly ILogger _logger;

    public ProviderAccountController(IGenericRepository repo, ILogger logger)
    {
      _repo = repo ?? throw new ArgumentNullException(nameof(repo));
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    // GET: api/provider-accounts/5
    [HttpGet("{providerId}", Name = "GetProviderAccountById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Get(Guid providerId)
    {
      _logger.LogInformation($"GET - Getting provider account by ID: {providerId}");
      var provider = await _repo.GetProviderAccountByIdAsync(providerId);
      if (provider == null)
      {
        _logger.LogWarning($"No provider account found for {providerId}");
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
        _logger.LogInformation($"POST - Post request started for new provider account. ID: {newProvider.ProviderId}\n Name: {newProvider.Name}");
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
        _logger.LogInformation($"Post request persisted for {newProvider.ProviderId}");
        return CreatedAtRoute("GetProviderAccountById", new { id = mappedProvider.ProviderId }, mappedProvider);
      }
      catch (Exception e)
      {
        _logger.LogError("Post request failed with exception: " + e.Message);
        return BadRequest();
      }
    }

    // PUT: api/provider-accounts/5
    [HttpPut("{providerId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(Guid providerId, [FromBody] ProviderAccount provider)
    {
      _logger.LogInformation($"PUT - Put request for provider ID: {providerId}");
      var existingProvider = await _repo.GetProviderAccountByIdAsync(providerId);
      if (existingProvider != null)
      {
        existingProvider.Name = provider.Name;
        existingProvider.AccountCreatedAt = DateTime.Now;
        await _repo.UpdateProviderAccountAsync(existingProvider);
        await _repo.SaveAsync();
        _logger.LogInformation($"Put request persisted for {providerId}");
        return NoContent();
      }
      _logger.LogWarning($"Put request failed for {providerId}");
      return NotFound();
    }

    // DELETE: api/provider-accounts/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid providerId)
    {
      _logger.LogInformation($"DELETE - Delete request for provider ID: {providerId}");
      var existingProvider = await _repo.GetProviderAccountByIdAsync(providerId);
      if (existingProvider != null)
      {
        await _repo.DeleteProviderAccountAsync(providerId);
        await _repo.SaveAsync();
        _logger.LogInformation($"Delete request persisted for {providerId}");
        return NoContent();
      }
      _logger.LogWarning($"Delete request failed for {providerId}");
      return NotFound();
    }
  }
}
