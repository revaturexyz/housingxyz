using Revature.Account.Lib.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Revature.Account.Lib.Interface
{
  public interface IGenericRepository
  {
    #region Provider Account Repositories

    public Task<ProviderAccount> GetProviderAccountByIdAsync(Guid providerId);

    public void AddProviderAccountAsync(ProviderAccount newAccount);

    public Task<bool> UpdateProviderAccountAsync(ProviderAccount providerAccount);

    public Task<bool> DeleteProviderAccountAsync(Guid providerId);

    #endregion Provider Account Repositories

    #region Coordinator

    public Task<CoordinatorAccount> GetCoordinatorAccountByIdAsync(Guid coordinatorId);

    // public Task UpdateCoordinatorAccount(CoordinatorAccount coordinatorAccount);
    // public Task DeleteCoordinatorAccount(Guid coordinatorId);

    #endregion Coordinator

    #region Notification

    public Task<Notification> GetNotificationByIdAsync(Guid notificationId);

    public Task<List<Notification>> GetNotificationsByCoordinatorIdAsync(Guid coordinatorId);

    public void AddNotification(Notification newNofi);

    public Task<bool> UpdateNotificationAsync(Notification notification);

    public Task<bool> DeleteNotificationByIdAsync(Guid providerId);

    #endregion Notification

    public Task SaveAsync();
  }
}
