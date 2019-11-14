using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xyz.Provider.DataAccess.Entities;
using Xyz.Provider.Lib.Exceptions;
using Xyz.Provider.Lib.Interface;

namespace Xyz.Provider.DataAccess.Repository
{
  public class NotificationRepository : INotificationRepository
  {
    private readonly ILogger _logger;
    private readonly RevatureHousingDbContext _dbContext;

    public NotificationRepository(RevatureHousingDbContext dbContext, ILogger<NotificationRepository> logger = null)
    {
      _dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
      _logger = logger;
    }

    public async Task<Lib.Models.Notification> PostRequestAsync(Lib.Models.Notification newNotification)
    {
      if (string.IsNullOrEmpty(newNotification.Title))
      {
        _logger?.LogWarning("Title can't be null or empty.");
        throw new ArgumentException($"Notification title \"{newNotification.Title}\" cannot be null or empty", nameof(newNotification));
      }
      if (newNotification.Room is null)
      {
        _logger?.LogWarning("Room can't be null.");
        throw new ArgumentException($"Notification room cannot be null", nameof(newNotification));
      }
      if (newNotification.Provider is null)
      {
        _logger?.LogWarning("Provider can't be null.");
        throw new ArgumentException($"Notification provider cannot be null", nameof(newNotification));
      }

      var roomId = newNotification.Room.RoomId;
      if (roomId <= 0)
      {
        _logger?.LogWarning("Invalid ID input.");
        throw new ArgumentException($"Room ID {roomId} is invalid.", nameof(newNotification));
      }
      var providerId = newNotification.Provider.ProviderId;
      if (providerId <= 0)
      {
        _logger?.LogWarning("Invalid ID input.");
        throw new ArgumentException($"Provider ID {providerId} is invalid.", nameof(newNotification));
      }

      var room = await _dbContext.Room
        .Include(r => r.Complex)
        .FirstOrDefaultAsync(r => r.RoomId == roomId)
        .ConfigureAwait(false);
      if (room is null)
      {
        _logger?.LogWarning("Couldn't find data with same ID.");
        throw new ArgumentNotFoundException("Room", roomId, nameof(roomId));
      }
      if (!await _dbContext.Provider.AnyAsync(p => p.ProviderId == providerId).ConfigureAwait(false))
      {
        _logger?.LogWarning("Couldn't find data with same ID.");
        throw new ArgumentNotFoundException("Provider", providerId, nameof(providerId));
      }
      if (room.Complex.ProviderId != providerId)
      {
        _logger?.LogWarning("Doesn't have permission to modify the room.");
        throw new InvalidOperationException($"Provider {providerId} doesn't have permission to modify room {roomId} under provider {room.Complex.ProviderId}.");
      }

      try
      {
        var entityNewNotification = new Notification
        {
          Title = newNotification.Title,
          Reason = newNotification.Reason ?? "",
          Room = room,
          ProviderId = providerId
        };
        _dbContext.Notification.Add(entityNewNotification);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        newNotification.NotificationId = entityNewNotification.NotificationId;
        return newNotification;
      }
      catch (DbUpdateException e)
      {
        _logger?.LogWarning($"Critical error: {e.Message}");
        throw;
      }
    }

    public async Task<IEnumerable<Lib.Models.Notification>> GetNotificationsAsync(int providerId)
    {
      if (providerId <= 0)
      {
        _logger?.LogWarning($"{providerId} is not a valid ID.");
        throw new ArgumentOutOfRangeException(nameof(providerId), providerId, "ID must be positive.");
      }
      if (!await _dbContext.Provider.AnyAsync(p => p.ProviderId == providerId).ConfigureAwait(false))
      {
        _logger?.LogWarning($"{providerId} is not found in database.");
        throw new ArgumentNotFoundException("Provider", providerId, nameof(providerId));
      }
      var notifications = await _dbContext.Notification
          .Where(n => n.ProviderId == providerId)
          .ToListAsync()
          .ConfigureAwait(false);
      var result = notifications.Select(Mapper.Map);
      return result;
    }
  }
}
