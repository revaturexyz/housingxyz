using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Revature.Account.Lib.Interface;
using System;
using System.Threading.Tasks;

namespace Revature.Account.Api.Controllers
{
  [Route("api/coordinator-accounts")]
  [ApiController]
  public class CoordinatorAccountController : ControllerBase
  {
    private readonly IGenericRepository _repo;

    public CoordinatorAccountController(IGenericRepository repo)
    {
      _repo = repo ?? throw new ArgumentNullException(nameof(repo));
    }

    // GET: api/coordinator-accounts/5
    [HttpGet("{coordinatorId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Get(Guid coordinatorId)
    {
      var coordinator = await _repo.GetCoordinatorAccountByIdAsync(coordinatorId);
      if (coordinator == null)
      {
        return NotFound();
      }
      return Ok(coordinator);
    }
  }
}
