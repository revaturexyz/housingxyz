using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Revature.Account.Api.Models;
using Revature.Account.Lib.Interface;

namespace Revature.Account.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CoordinatorAccountController : ControllerBase
  {
    private readonly IGenericRepository _repo;

    public CoordinatorAccountController(IGenericRepository urepo)
    {
      _repo = urepo ?? throw new ArgumentNullException("Cannot be null.", nameof(urepo));
    }

    // GET: api/CoordinatorAccount/5
    [HttpGet]
    public async Task<ActionResult> Get(Guid coordinatorId)
    {
      var x = await _repo.GetCoordinatorAccountById(coordinatorId);
      if (x == null)
      {
        return NotFound();
      }
      return Ok(new CoordinatorViewModel()
      {
        CoordinatorId = x.CoordinatorId,
        Email = x.Email,
        Password = x.Password,
        TrainingName = x.TrainingName,
        TrainingAddress = x.TrainingAddress,
      });
    }
  }
}
