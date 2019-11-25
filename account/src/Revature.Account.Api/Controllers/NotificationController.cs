using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Revature.Account.Lib.Interface;
using Revature.Account.Lib.Model;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Revature.Account.Api.Controllers
{
  /// <summary>
  /// RESTful API Controllers for the Notifications
  /// </summary>
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
      _logger.LogInformation($"GET - Getting notifications by coordinator ID: {coordinatorId}");
      var nofi = await _repo.GetNotificationsByCoordinatorIdAsync(coordinatorId);
      if (nofi == null)
      {
        _logger.LogWarning($"Coordinator was not found: {coordinatorId}");
        return NotFound();
      }
      return Ok(nofi);
    }

    // POST: api/notifications
    [HttpPost]
    [ProducesResponseType(typeof(Notification), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody, Bind("ProviderId, CoordinatorId")] Notification notification)
    {
      try
      {
        _logger.LogInformation($"POST - Making notification for notification ID {notification.NotificationId}." +
          $" Provider ID: {notification.ProviderId}\n Coordinator ID: {notification.CoordinatorId}");
        Lib.Model.Notification mappedNotification = new Lib.Model.Notification()
        {
          ProviderId = notification.ProviderId,
          CoordinatorId = notification.CoordinatorId,
          // Set Status to 'Approved'
          Status = await _repo.GetStatusByStatusTextAsync("Approved"),
          AccountExpiresAt = DateTime.Now.AddDays(7)
        };
        _repo.AddNotification(mappedNotification);
        await _repo.SaveAsync();
        _logger.LogInformation($"Persisted notification {notification.NotificationId}");
        return CreatedAtRoute("GetNotificationsByCoordinatorId", new { id = mappedNotification.NotificationId }, mappedNotification);
      }
      catch(Exception e)
      {
        _logger.LogError("Post request failed with exception: " + e.Message);
        return BadRequest();
      }
    }

    // PUT: api/notifications/5
    [HttpPut("{notificationId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Put(Guid notificationId, [FromBody] Status notificationStatus)
    {
      _logger.LogInformation("PUT - Updating notification information for notification {notificationId}", notificationId);
      var existingNotification = await _repo.GetNotificationByIdAsync(notificationId);
      if (existingNotification != null)
      {
        existingNotification.Status = notificationStatus;
        // Status is 'Under Review' and the notification only has 7 days left
        if (existingNotification.Status.GetStatusId() == 4 && (DateTime.Today.Date - existingNotification.AccountExpiresAt.Date).Days <= 7)
        {
          existingNotification.AccountExpiresAt = DateTime.Now.AddDays(30);
        }
        // Status is 'Rejected'
        if (existingNotification.Status.GetStatusId() == 3)
        {
          await _repo.DeleteNotificationByIdAsync(notificationId);
          return NoContent();
        }
        await _repo.UpdateNotificationAsync(existingNotification);
        await _repo.SaveAsync();
        _logger.LogInformation("Persisted put request");
        return NoContent();
      }
      _logger.LogWarning("Put request failed");
      return NotFound();
    }

    // DELETE: api/notifications/5
    [HttpDelete("{notificationId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid notificationId)
    {
      _logger.LogInformation($"DELETE - Deleting notification with ID: {notificationId}");
      var existingNotification = await _repo.GetNotificationByIdAsync(notificationId);
      if (existingNotification != null)
      {
        await _repo.DeleteNotificationByIdAsync(notificationId);
        await _repo.SaveAsync();
        _logger.LogInformation($"Persisted delete request for {notificationId}");
        return NoContent();
      }
      _logger.LogWarning($"Delete request failed {notificationId}");
      return NotFound();
    }
  }
}
