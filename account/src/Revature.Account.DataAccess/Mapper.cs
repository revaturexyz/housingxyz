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
    /// Maps db Notification to logic Notification. Maps Coordinator, Provider, and
    /// UpdateAction as well.
    /// </summary>
    /// <param name="nofi"></param>
    /// <returns></returns>
    public Lib.Model.Notification MapNotification(Entities.Notification nofi)
    {
      return new Lib.Model.Notification
      {
        NotificationId = nofi.NotificationId,
        ProviderId = nofi.ProviderId,
        Provider = MapProvider(nofi.Provider),
        CoordinatorId = nofi.CoordinatorId,
        Coordinator = MapCoordinator(nofi.Coordinator),
        UpdateAction = MapUpdateAction(nofi.UpdateAction),
        AccountExpiresAt = nofi.AccountExpiresAt
      };
    }

    public Entities.Notification MapNotification(Lib.Model.Notification nofi)
    {
      return new Entities.Notification
      {
        NotificationId = nofi.NotificationId,
        ProviderId = nofi.Provider.ProviderId,
        CoordinatorId = nofi.Coordinator.CoordinatorId,
        UpdateActionId = nofi.UpdateAction.Id,
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

    public Lib.Model.UpdateAction MapUpdateAction(Entities.UpdateAction action)
    {
      return new Lib.Model.UpdateAction
      {
        Id = action.Id,
        NotificationId = action.NotificationId,
        UpdateType = action.UpdateType,
        SerializedTarget = action.SerializedTarget
      };
    }

    public Entities.UpdateAction MapUpdateAction(Lib.Model.UpdateAction action)
    {
      return new Entities.UpdateAction
      {
        Id = action.Id,
        NotificationId = action.NotificationId,
        UpdateType = action.UpdateType,
        SerializedTarget = action.SerializedTarget
      };
    }
  }
}
