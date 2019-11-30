using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Revature.Account.Lib.Interface;
using Revature.Account.Lib.Model;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using System.Linq;
using Auth0.ManagementApi;
using Microsoft.AspNetCore.Authorization;

namespace Revature.Account.Api.Controllers
{
  /// <summary>
  /// RESTful API Controllers for the Provider account.
  /// </summary>
  [Route("api/provider-accounts")]
  [ApiController]
  [Authorize]
  public class ProviderAccountController : ControllerBase
  {
    private readonly IGenericRepository _repo;
    private readonly ILogger<ProviderAccountController> _logger;
    private readonly IAuth0HelperFactory _authHelperFactory;

    public ProviderAccountController(IGenericRepository repo, ILogger<ProviderAccountController> logger, IAuth0HelperFactory authHelperFactory)
    {
      _repo = repo ?? throw new ArgumentNullException(nameof(repo));
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      _authHelperFactory = authHelperFactory;
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
    [ProducesResponseType(typeof(ProviderAccount), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody, Bind("CoordinatorId, Name, Email")] ProviderAccount newProvider)
    {
      try
      {
        _logger.LogInformation($"POST - Post request started for new provider account. ID: {newProvider.ProviderId}\n Name: {newProvider.Name}");

        // Create a provider
        Lib.Model.ProviderAccount mappedProvider = new Lib.Model.ProviderAccount
        {
          CoordinatorId = newProvider.CoordinatorId,
          Name = newProvider.Name,
          Email = newProvider.Email,
          Status = new Status(Status.Pending),
          AccountCreatedAt = DateTime.Now,
          AccountExpiresAt = DateTime.Now.AddDays(7)
        };
        _repo.AddProviderAccountAsync(mappedProvider);
        await _repo.SaveAsync();

        // Create a notification for the provider's approval
        Notification newNotification = new Lib.Model.Notification
        {
          CoordinatorId = newProvider.CoordinatorId ?? Guid.Empty,
          ProviderId = mappedProvider.ProviderId,
          Status = new Status(Status.Pending),
          UpdateAction = new UpdateAction
          {
            UpdateType = "ApproveProviderAccount",
            SerializedTarget = JsonSerializer.Serialize(mappedProvider)
          },
          AccountExpiresAt = DateTime.Now
        };
        _repo.AddNotification(newNotification);
        await _repo.SaveAsync();

        _logger.LogInformation($"Post request persisted for {newProvider.ProviderId}");
        return CreatedAtRoute("GetProviderAccountById", new { mappedProvider.ProviderId }, mappedProvider);
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
    public async Task<IActionResult> Put(Guid providerId, [FromBody, Bind("CoordinatorId, Name, Email")] ProviderAccount provider)
    {
      _logger.LogInformation($"PUT - Put request for provider ID: {providerId}");
      var existingProvider = await _repo.GetProviderAccountByIdAsync(providerId);
      if (existingProvider != null)
      {
        existingProvider.CoordinatorId = provider.CoordinatorId;
        existingProvider.Name = provider.Name;
        existingProvider.Email = provider.Email;

        await _repo.UpdateProviderAccountAsync(existingProvider);
        await _repo.SaveAsync();
        _logger.LogInformation($"Put request persisted for {providerId}");
        return NoContent();
      }
      _logger.LogWarning($"Put request failed for {providerId}");
      return NotFound();
    }

    // PUT: api/provider-accounts/approve
    [HttpPut("{providerId}/approve")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(Guid providerId)
    {
      _logger.LogInformation($"PUT - Approval request for provider: {providerId}");
      var existingProvider = await _repo.GetProviderAccountByIdAsync(providerId);
      if (existingProvider != null && existingProvider.Status.StatusText != Status.Approved)
      {
        Auth0Helper auth0 = _authHelperFactory.Create(Request);
        var authUser = await auth0.Client.Users.GetUsersByEmailAsync(auth0.Email);

        // Remove unapproved_provider role
        if (auth0.Roles.Contains(Auth0Helper.UnapprovedProviderRole))
        {
          await auth0.RemoveRoleAsync(authUser[0].UserId, Auth0Helper.UnapprovedProviderRole);
        }

        // Add approved_provider 
        await auth0.AddRoleAsync(authUser[0].UserId, Auth0Helper.ApprovedProviderRole);

        existingProvider.Status.StatusText = Status.Approved;
        await _repo.UpdateProviderAccountAsync(existingProvider);
        await _repo.SaveAsync();

        _logger.LogInformation($"Approval set to true for id: {providerId}");
        return NoContent();
      }

      _logger.LogWarning($"Account already is approved, no change for id: {providerId}");
      return NotFound();
    }

    // DELETE: api/provider-accounts/5
    [HttpDelete("{providerId}")]
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
