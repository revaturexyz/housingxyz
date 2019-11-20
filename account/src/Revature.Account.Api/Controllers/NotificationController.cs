using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Revature.Account.Lib.Interface;
using Revature.Account.Lib.Model;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Revature.Account.Api.Controllers
{
  [Route("api/notifications")]
  [ApiController]
  public class NotificationController : ControllerBase
  {
    private readonly IGenericRepository _repo;
    private readonly ILogger _logger;

    public NotificationController(IGenericRepository repo, ILogger logger)
    {
      _repo = repo ?? throw new ArgumentNullException(nameof(repo));
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    // GET: api/notifications/5
    [HttpGet("{coordinatorId}", Name = "GetNotificationsByCoordinatorId")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetNotificationByCoordinatorIdAsync(Guid coordinatorId)
    {
      _logger.LogInformation("GET - Getting notifications by coordinator ID: {coordinatorId}", coordinatorId);
      var nofi = await _repo.GetNotificationsByCoordinatorIdAsync(coordinatorId);
      if (nofi == null)
      {
        _logger.LogWarning("Coordinator was not found");
        return NotFound();
      }
      return Ok(nofi);
    }

    // POST: api/notifications
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody, Bind("ProviderId, CoordinatorId")] Notification notification)
    {
      try
      {
        _logger.LogInformation("POST - Making notification");
        Lib.Model.Notification mappedNotification = new Lib.Model.Notification()
        {
          ProviderId = notification.ProviderId,
          CoordinatorId = notification.CoordinatorId,
          // Set Status to 'Approved'
          Status = await _repo.GetStatusByIdAsync(1),
          AccountExpiresAt = DateTime.Now.AddDays(7)
        };
        _repo.AddNotification(mappedNotification);
        await _repo.SaveAsync();
        _logger.LogInformation("Persisted post request");
        return CreatedAtRoute("GetNotificationsByCoordinatorId", new { id = mappedNotification.NotificationId }, mappedNotification);
      }
      catch
      {
        _logger.LogError("Post request failed");
        return BadRequest();
      }
    }

    // PATCH: api/notifications/5
    [HttpPatch("{coordinatorId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Patch(Guid coordinatorId, [FromBody] Notification notification)
    {
      _logger.LogInformation("PATCH - Patching notification information");
      var existingNotification = await _repo.GetNotificationByIdAsync(coordinatorId);
      if (existingNotification != null)
      {
        existingNotification.Status = notification.Status;
        // Status is 'Under Review;
        if (existingNotification.Status.StatusId == 4)
        {
          existingNotification.AccountExpiresAt = DateTime.Now.AddDays(30);
        }
        // Status is 'Rejected'
        if (existingNotification.Status.StatusId == 3)
        {
          existingNotification = null;
        }
        await _repo.UpdateNotificationAsync(existingNotification);
        await _repo.SaveAsync();
        _logger.LogInformation("Persisted patch request");
        return NoContent();
      }
      _logger.LogWarning("Patch request failed");
      return NotFound();
    }

    // DELETE: api/notifications/5
    [HttpDelete("{notificationId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid notificationId)
    {
      _logger.LogInformation("DELETE - Deleting notification");
      var existingNotification = await _repo.GetProviderAccountByIdAsync(notificationId);
      if (existingNotification != null)
      {
        await _repo.DeleteNotificationByIdAsync(notificationId);
        await _repo.SaveAsync();
        _logger.LogInformation("Persisted delete request");
        return NoContent();
      }
      _logger.LogWarning("Delete request failed");
      return NotFound();
    }
  }
}
