using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Revature.Account.Lib.Model;

namespace Revature.Account.Lib.Interface
{
  public interface IGenericRepository 
  {
    #region Provider Account Repositories
    public Task<ProviderAccount> GetProviderAccountByIdAsync(Guid providerId);
    public void AddNewProviderAccountAsync(ProviderAccount newAccount);
    public Task<bool> UpdateProviderAccountAsync(ProviderAccount providerAccount);
    public Task<bool> DeleteProviderAccountAsync(Guid providerId);
    #endregion
    #region Coordinator
    public Task<CoordinatorAccount> GetCoordinatorAccountByIdAsync(Guid coordinatorId);
    //public Task UpdateCoordinatorAccount(CoordinatorAccount coordinatorAccount);
    //public Task DeleteCoordinatorAccount(Guid coordinatorId);
    #endregion
    #region Notification
    public Task<Notification> GetNotificationByIdAsync(Guid notificationId);
    public Task<List<Notification>> GetNotificationsByCoordinatorIdAsync(Guid coordinatorId);
    public void AddNewNotification(Notification newNofi);
    public Task<bool> UpdateNotificationAsync(Notification notification);
    public Task<bool> DeleteNotificationByIdAsync(Guid providerId);
    #endregion
    public Task SaveAsync();
  }
}
