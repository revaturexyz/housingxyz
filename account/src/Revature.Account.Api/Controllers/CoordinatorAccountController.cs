using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Revature.Account.Lib.Interface;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Revature.Account.Api.Controllers
{
  /// <summary>
  /// RESTful API Controllers for the Coordinator account.
  /// </summary>
  [Route("api/coordinator-accounts")]
  [ApiController]
  public class CoordinatorAccountController : ControllerBase
  {
    private readonly IGenericRepository _repo;
    private readonly ILogger<CoordinatorAccountController> _logger;

    public CoordinatorAccountController(IGenericRepository repo, ILogger<CoordinatorAccountController> logger)
    {
      _repo = repo ?? throw new ArgumentNullException(nameof(repo));
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    }

    // GET: api/coordinator-accounts/5
    [HttpGet("{coordinatorId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Get(Guid coordinatorId)
    {
      _logger.LogInformation($"GET - Getting coordinator with ID: {coordinatorId}");
      var coordinator = await _repo.GetCoordinatorAccountByIdAsync(coordinatorId);

      if (coordinator == null)
      {
        _logger.LogWarning($"No coordinator found for given ID: {coordinatorId} on GET call.");
        return NotFound();
      }

      return Ok(coordinator);
    }
  }
}
