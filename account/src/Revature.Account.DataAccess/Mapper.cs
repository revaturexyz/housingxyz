using System.Linq;

namespace Revature.Account.DataAccess
{
  /// <summary>
  /// Maps between the business logic and data access layers.
  /// </summary>
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
        CoordinatorId = provider.CoordinatorId,
        Name = provider.Name,
        Status = new Lib.Model.Status { StatusText = provider.StatusText },
        AccountCreatedAt = provider.AccountCreatedAt,
        AccountExpiresAt = provider.AccountExpiresAt
      };
    }

    public Entities.ProviderAccount MapProvider(Lib.Model.ProviderAccount provider)
    {
      return new Entities.ProviderAccount
      {
        ProviderId = provider.ProviderId,
        CoordinatorId = provider.CoordinatorId,
        Name = provider.Name,
        StatusText = provider.Status.StatusText,
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
        CoordinatorId = nofi.CoordinatorId,
        UpdateAction = MapUpdateAction(nofi.UpdateAction),
        Status = new Lib.Model.Status { StatusText = nofi.StatusText },
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
        UpdateActionId = nofi.UpdateAction.UpdateActionId,
        StatusText = nofi.Status.StatusText,
        AccountExpiresAt = nofi.AccountExpiresAt
      };
    }

    public Lib.Model.UpdateAction MapUpdateAction(Entities.UpdateAction action)
    {
      return new Lib.Model.UpdateAction
      {
        UpdateActionId = action.UpdateActionId,
        NotificationId = action.NotificationId,
        UpdateType = action.UpdateType,
        SerializedTarget = action.SerializedTarget
      };
    }

    public Entities.UpdateAction MapUpdateAction(Lib.Model.UpdateAction action)
    {
      return new Entities.UpdateAction
      {
        UpdateActionId = action.UpdateActionId,
        NotificationId = action.NotificationId,
        UpdateType = action.UpdateType,
        SerializedTarget = action.SerializedTarget
      };
    }
  }
}
