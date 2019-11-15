using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xyz.AccountService.Lib.Model;

namespace Xyz.AccountService.Lib.Interface
{
	public interface IGenericRepository : IAsyncDisposable
	{
		/* Provider Account Repositories */
		public Task<ProviderAccount> GetProviderAccountById(Guid providerId);
		public Task AddNewProviderAccount(ProviderAccount newAccount);
		public Task UpdateProviderAccount(ProviderAccount providerAccount);
		public Task DeleteProviderAccount(Guid providerId);
		/* End */

		/* Coordinator Account Repositories */
		public Task<CoordinatorAccount> GetCoordinatorAccountById(Guid coordinatorId);
		//public Task UpdateCoordinatorAccount(CoordinatorAccount coordinatorAccount);
		//public Task DeleteCoordinatorAccount(Guid coordinatorId);
		/* End */

		/* Notification Repositories */
		public Task<Notification> GetNotificationById(Guid providerId);
		public Task AddNewNotification(Notification newNofi);
		public Task UpdateNotification(Notification notification);
		public Task DeleteNotificationById(Guid providerId);
		/* End */

		public Task Save();

	}
}
