using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Revature.Account.Lib;

namespace Revature.Account.DataAccess
{
  public class Mapper
  {
    public Lib.Model.ProviderAccount MapProvider(Entities.ProviderAccount provider)
    {
      return new Lib.Model.ProviderAccount
      {
        ProviderId = provider.ProviderId,
        CoordinatorId = provider.CoordinatorId,
        Name = provider.Name,
        Password = provider.Password,
        Status = provider.Status,
        AccountCreated = provider.AccountCreated,
        Expire = provider.Expire
      };
    }
    public Entities.ProviderAccount MapProvider(Lib.Model.ProviderAccount provider)
    {
      return new Entities.ProviderAccount
      {
        ProviderId = provider.ProviderId,
        CoordinatorId = provider.CoordinatorId,
        Name = provider.Name,
        Password = provider.Password,
        Status = provider.Status,
        AccountCreated = provider.AccountCreated,
        Expire = provider.Expire
      };
    }
    public Lib.Model.CoordinatorAccount MapCoordinator(Entities.CoordinatorAccount coordinator)
    {
      return new Lib.Model.CoordinatorAccount
      {
        CoordinatorId = coordinator.CoordinatorId,
        Name = coordinator.Name,
        Password = coordinator.Password,
        TrainingName = coordinator.TrainingName,
        TrainingAddress = coordinator.TrainingAddress,
        Email = coordinator.Email,
        Notifications = coordinator.Notifications.Select(MapNotification).ToList()
      };
    }
    public static Entities.CoordinatorAccount MapCoordinator(Lib.Model.CoordinatorAccount coordinator)
    {
      return new Entities.CoordinatorAccount
      {
        CoordinatorId = coordinator.CoordinatorId,
        Name = coordinator.Name,
        Password = coordinator.Password,
        TrainingName = coordinator.TrainingName,
        TrainingAddress = coordinator.TrainingAddress,
        Email = coordinator.Email
      };
    }
    public Lib.Model.Notification MapNotification(Entities.Notification nofi)
    {
      return new Lib.Model.Notification
      {
        NotificationId = nofi.NotificationId,
        ProviderId = nofi.ProviderId,
        CoordinatorId = nofi.CoordinatorId,
        Status = nofi.Status,
        AccountExpire = nofi.AccountExpire
      };
    }
    public Entities.Notification MapNotification(Lib.Model.Notification nofi)
    {
      return new Entities.Notification
      {
        NotificationId = nofi.NotificationId,
        ProviderId = nofi.ProviderId,
        CoordinatorId = nofi.CoordinatorId,
        Status = nofi.Status,
        AccountExpire = nofi.AccountExpire
      };
    }
  }
}
