using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Revature.Account.Lib.Model;

namespace Revature.Account.Lib.Interface
{
  public interface IGenericRepository : IAsyncDisposable
  {
    #region Provider Account Repositories
    public Task<ProviderAccount> GetProviderAccountByIdAsync(Guid providerId);
    public void AddNewProviderAccountAsync(ProviderAccount newAccount);
    public Task UpdateProviderAccountAsync(ProviderAccount providerAccount);
    public Task DeleteProviderAccountAsync(Guid providerId);
    #endregion
    #region
    public Task<CoordinatorAccount> GetCoordinatorAccountByIdAsync(Guid coordinatorId);
    //public Task UpdateCoordinatorAccount(CoordinatorAccount coordinatorAccount);
    //public Task DeleteCoordinatorAccount(Guid coordinatorId);
    #endregion
    #region
    public Task<Notification> GetNotificationByIdAsync(Guid providerId);
    public void AddNewNotification(Notification newNofi);
    public Task UpdateNotificationAsync(Notification notification);
    public Task DeleteNotificationByIdAsync(Guid providerId);
    #endregion
    public Task SaveAsync();
  }
}
