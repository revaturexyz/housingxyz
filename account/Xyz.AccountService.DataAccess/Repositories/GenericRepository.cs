using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xyz.AccountService.Lib.Interface;
using Xyz.AccountService.Lib.Model;

namespace Xyz.AccountService.DataAccess.Repositories
{
	public class GenericRepository : IGenericRepository
	{
		private readonly AccountServiceDbContext _context;

		public GenericRepository(AccountServiceDbContext db)
		{
			_context = db ?? throw new ArgumentNullException("Context cannot be null.", nameof(db));
		}

		/* Provider Repos */
		public Task<ProviderAccount> GetProviderAccountById(Guid providerId)
		{
			throw new NotImplementedException();
		}
		public Task AddNewProviderAccount(ProviderAccount newAccount)
		{
			throw new NotImplementedException();
		}
		public Task UpdateProviderAccount(ProviderAccount providerAccount)
		{
			throw new NotImplementedException();
		}
		public Task DeleteProviderAccount(Guid providerId)
		{
			throw new NotImplementedException();
		}	
		/* End */

		/* Coordinator Repos */
		public Task<CoordinatorAccount> GetCoordinatorAccountById(Guid coordinatorId)
		{
			throw new NotImplementedException();
		}
		/* End */

		/* Notification Repos */
		public Task<Notification> GetNotificationById(Guid providerId)
		{
			throw new NotImplementedException();
		}
		public Task AddNewNotification(Notification newNofi)
		{
			throw new NotImplementedException();
		}
		public Task DeleteNotificationById(Guid providerId)
		{
			throw new NotImplementedException();
		}
		public Task UpdateNotification(Notification notification)
		{
			throw new NotImplementedException();
		}
		/* End */

		public Task Save()
		{
			throw new NotImplementedException();
		}
		public ValueTask DisposeAsync()
		{
			throw new NotImplementedException();
		}
	}
}
