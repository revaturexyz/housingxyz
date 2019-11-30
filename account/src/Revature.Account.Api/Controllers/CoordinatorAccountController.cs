using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Revature.Account.Lib.Interface;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Linq;
using Revature.Account.Lib.Model;

namespace Revature.Account.Api.Controllers
{
  /// <summary>
  /// RESTful API Controllers for the Coordinator account.
  /// </summary>
  [Route("api/coordinator-accounts")]
  [ApiController]
  //[Authorize]
  public class CoordinatorAccountController : ControllerBase
  {
    private readonly IGenericRepository _repo;
    private readonly ILogger<CoordinatorAccountController> _logger;
    private readonly IAuth0HelperFactory _authHelperFactory;

    public CoordinatorAccountController(IGenericRepository repo, ILogger<CoordinatorAccountController> logger, IAuth0HelperFactory authHelperFactory)
    {
      _repo = repo ?? throw new ArgumentNullException(nameof(repo));
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      _authHelperFactory = authHelperFactory;
    }

    // NOTE: You literally call ...accounts/id, not with any particular id
    // GET: api/coordinator-accounts/id
    [HttpGet("id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Get()
    {
      _logger.LogInformation($"GET - Retrieving user ID and verifying correct metadata is in token.");

      try
      {
        Auth0Helper auth0 = _authHelperFactory.Create(Request);
        var authUser = await auth0.Client.Users.GetUsersByEmailAsync(auth0.Email);
        var authRoles = await auth0.Client.Roles.GetAllAsync(new Auth0.ManagementApi.Models.GetRolesRequest());

        Guid id = Guid.Empty;

        id = await _repo.GetCoordinatorIdByEmailAsync(auth0.Email);
        if (id != Guid.Empty)
        {
          // If their roles arent set properly, set them
          if (!auth0.Roles.Contains(Auth0Helper.CoordinatorRole))
          {
            await auth0.AddRole(authUser[0].UserId, authRoles.First(r => r.Name == Auth0Helper.CoordinatorRole).Id);
          }
        }
        else
        {
          // Check the provider db
          id = await _repo.GetProviderIdByEmailAsync(auth0.Email);

          if (id != Guid.Empty)
          {
            // Set roles if not set already
            if (!auth0.Roles.Contains(Auth0Helper.UnapprovedProviderRole) && !auth0.Roles.Contains(Auth0Helper.ApprovedProviderRole))
            {
              // They have no role, so set them as unapproved
              await auth0.AddRole(authUser[0].UserId, authRoles.First(r => r.Name == Auth0Helper.UnapprovedProviderRole).Id);
            }
          }
        }

        if (id == Guid.Empty)
        {
          // They have no account anywhere - check roles for coordinator role
          if (auth0.Roles.Contains(Auth0Helper.CoordinatorRole))
          {
            // They have been set as a coordinator on the Auth0 site, so make a new account
            var coordinator = new CoordinatorAccount
            {
              Name = (authUser[0].FirstName != null && authUser[0].LastName != null
                ? authUser[0].FirstName + " " + authUser[0].LastName
                : "No Name"),
              Email = auth0.Email
            };
            // Add them
            _repo.AddCoordinatorAccount(coordinator);
          }
          else
          {
            // Make a new provider
            ProviderAccount provider = new ProviderAccount
            {
              Name = (authUser[0].FirstName != null && authUser[0].LastName != null
                ? authUser[0].FirstName + " " + authUser[0].LastName
                : "No Name"),
              Email = auth0.Email,
              Status = new Status(Status.Pending),
              AccountCreatedAt = DateTime.Now,
              AccountExpiresAt = DateTime.Now.AddDays(7)
            };
            // Add them
            _repo.AddProviderAccountAsync(provider);

            // They have no role, so set them as unapproved
            await auth0.AddRole(authUser[0].UserId, authRoles.First(r => r.Name == Auth0Helper.UnapprovedProviderRole).Id);
          }
          // Db was modified either way, save changes
          await _repo.SaveAsync();

          // Get their id
          id = await _repo.GetProviderIdByEmailAsync(auth0.Email);
        }

        // Update the app_metadata if it doesnt contain the correct id
        await auth0.UpdateMetadataWithId(authUser[0].UserId, id);

        return Ok(id);
      }
      catch (Exception e)
      {
        _logger.LogError("Error occured in token setup: {error}", e);
        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
      }
    }

    // GET: api/coordinator-accounts/5
    [HttpGet("{coordinatorId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Get(Guid coordinatorId)
    {
      try
      {
        _logger.LogInformation("GET - Getting coordinator with ID: {coordinatorId}", coordinatorId);
        var coordinator = await _repo.GetCoordinatorAccountByIdAsync(coordinatorId);

        if (coordinator == null)
        {
          _logger.LogWarning("No coordinator found for given ID: {coordinatorId} on GET call.", coordinatorId);
          return NotFound();
        }

        return Ok(coordinator);
      }
      catch (Exception e)
      {
        _logger.LogError("Exception getting coordinator: {exceptionMessage}, {exception}", e.Message, e);
        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
      }
    }

    // GET: api/coordinator-accounts/all
    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetAll()
    {
      try
      {
        _logger.LogInformation("GET - Retreiving all coordinators.");
        var coordinators = await _repo.GetAllCoordinatorAccountsAsync();

        return Ok(coordinators);
      }
      catch (Exception e)
      {
        _logger.LogError("Exception occurred in GetAll for coordinator controller: {exceptionMessage}, {exception}", e.Message, e);
        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
      }
    }
  }
}
