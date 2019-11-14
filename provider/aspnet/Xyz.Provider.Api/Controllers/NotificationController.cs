using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xyz.Provider.Api.Models;
using Xyz.Provider.Lib.Interface;
using Xyz.Provider.Lib.Models;

namespace Xyz.Provider.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class NotificationController : ControllerBase
  {
    private readonly INotificationRepository _notificationRepository;

    public NotificationController(INotificationRepository notificationRepository)
    {
      _notificationRepository = notificationRepository ??
        throw new ArgumentNullException(nameof(notificationRepository), "Provider repo cannot be null");
    }

    /// <summary>
    /// Posts a notification
    /// </summary>
    /// <param name="notification"></param>
    /// <returns></returns>
    // POST: api/notification
    [HttpPost]
    public async Task<ActionResult> PostAsync([FromBody]ApiNotification notification)
    {
      try
      {
        var newNotification = new Notification
        {
          Provider = new Lib.Models.Provider { ProviderId = notification.ProviderId },
          Room = new Room { RoomId = notification.RoomId },
          Title = notification.Title,
          Reason = notification.Reason
        };
        var result = await _notificationRepository.PostRequestAsync(newNotification);
        return Ok(result);
      }
      catch (ArgumentException e)
      {
        return BadRequest(e.Message);
      }
      catch (InvalidOperationException e)
      {
        return Forbid(e.Message);
      }
      catch (Exception e)
      {
        return StatusCode(500, e.Message);
      }
    }

    /// <summary>
    /// Gets notifications using a provider ID
    /// </summary>
    /// <param name="providerId"></param>
    /// <returns></returns>
    // GET: api/notification/5
    [HttpGet("{providerId}")]
    public async Task<ActionResult> GetByProviderIdAsync([FromRoute]int providerId)
    {
      try
      {
        var result = await _notificationRepository.GetNotificationsAsync(providerId);
        return Ok(result);
      }
      catch (ArgumentException)
      {
        return NotFound();
      }
      catch (Exception e)
      {
        return StatusCode(500, e.Message);
      }
    }
  }
}
