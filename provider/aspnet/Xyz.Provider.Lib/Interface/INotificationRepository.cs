using System.Collections.Generic;
using System.Threading.Tasks;
using Xyz.Provider.Lib.Models;

namespace Xyz.Provider.Lib.Interface
{
  public interface INotificationRepository
  {
    Task<Notification> PostRequestAsync(Notification newNotification);
    Task<IEnumerable<Notification>> GetNotificationsAsync(int providerId);
  }
}
