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
		public Task<ProviderAccount> GetProviderAccountById(Guid providerId);
		public void AddNewProviderAccount(ProviderAccount newAccount);
		public Task UpdateProviderAccount(ProviderAccount providerAccount);
		public Task DeleteProviderAccount(Guid providerId);
		#endregion
		#region
		public Task<CoordinatorAccount> GetCoordinatorAccountById(Guid coordinatorId);
		//public Task UpdateCoordinatorAccount(CoordinatorAccount coordinatorAccount);
		//public Task DeleteCoordinatorAccount(Guid coordinatorId);
		#endregion
		#region
		public Task<Notification> GetNotificationById(Guid providerId);
		public void AddNewNotification(Notification newNofi);
		public Task UpdateNotification(Notification notification);
		public Task DeleteNotificationById(Guid providerId);
		#endregion

		public Task Save();

	}
}
