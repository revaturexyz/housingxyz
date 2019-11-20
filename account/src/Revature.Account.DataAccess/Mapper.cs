using System.Linq;

namespace Revature.Account.DataAccess
{
  public class Mapper
  {
    /// <summary>
    /// Maps db Provider to logic Provider. Maps related coordinator and Status as well.
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public Lib.Model.ProviderAccount MapProvider(Entities.ProviderAccount provider)
    {
      return new Lib.Model.ProviderAccount
      {
        ProviderId = provider.ProviderId,
        Coordinator = MapCoordinator(provider.Coordinator),
        Name = provider.Name,
        Status = MapStatus(provider.Status),
        AccountCreatedAt = provider.AccountCreatedAt,
        AccountExpiresAt = provider.AccountExpiresAt
      };
    }

    public Entities.ProviderAccount MapProvider(Lib.Model.ProviderAccount provider)
    {
      return new Entities.ProviderAccount
      {
        ProviderId = provider.ProviderId,
        CoordinatorId = provider.Coordinator.CoordinatorId,
        Name = provider.Name,
        StatusId = provider.Status.StatusId,
        AccountCreatedAt = provider.AccountCreatedAt,
        AccountExpiresAt = provider.AccountExpiresAt
      };
    }

    /// <summary>
    /// Maps db Coordinator to logic Coordinator. Maps related list of outstanding
    /// Notifications as well.
    /// </summary>
    /// <param name="coordinator"></param>
    /// <returns></returns>
    public Lib.Model.CoordinatorAccount MapCoordinator(Entities.CoordinatorAccount coordinator)
    {
      return new Lib.Model.CoordinatorAccount
      {
        CoordinatorId = coordinator.CoordinatorId,
        Name = coordinator.Name,
        Email = coordinator.Email,
        TrainingCenterName = coordinator.TrainingCenterName,
        TrainingCenterAddress = coordinator.TrainingCenterAddress,
        Notifications = coordinator.Notifications.Select(MapNotification).ToList()
      };
    }

    public Entities.CoordinatorAccount MapCoordinator(Lib.Model.CoordinatorAccount coordinator)
    {
      return new Entities.CoordinatorAccount
      {
        CoordinatorId = coordinator.CoordinatorId,
        Name = coordinator.Name,
        Email = coordinator.Email,
        TrainingCenterName = coordinator.TrainingCenterName,
        TrainingCenterAddress = coordinator.TrainingCenterAddress
      };
    }

    /// <summary>
    /// Maps db Notification to logic Notification. Maps Status as well.
    /// </summary>
    /// <param name="nofi"></param>
    /// <returns></returns>
    public Lib.Model.Notification MapNotification(Entities.Notification nofi)
    {
      return new Lib.Model.Notification
      {
        NotificationId = nofi.NotificationId,
        ProviderId = nofi.ProviderId,
        CoordinatorId = nofi.CoordinatorId,
        Status = MapStatus(nofi.Status),
        AccountExpiresAt = nofi.AccountExpiresAt
      };
    }

    public Entities.Notification MapNotification(Lib.Model.Notification nofi)
    {
      return new Entities.Notification
      {
        NotificationId = nofi.NotificationId,
        ProviderId = nofi.ProviderId,
        CoordinatorId = nofi.CoordinatorId,
        StatusId = nofi.Status.StatusId,
        AccountExpiresAt = nofi.AccountExpiresAt
      };
    }

    /// <summary>
    /// Maps db Status to logic Status. No nested objects.
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public Lib.Model.Status MapStatus(Entities.Status status)
    {
      return new Lib.Model.Status
      {
        StatusId = status.StatusId,
        StatusText = status.StatusText
      };
    }

    public Entities.Status MapStatus(Lib.Model.Status status)
    {
      return new Entities.Status
      {
        StatusId = status.StatusId,
        StatusText = status.StatusText
      };
    }
  }
}
