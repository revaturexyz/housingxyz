using Microsoft.EntityFrameworkCore;
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
		public async Task<ProviderAccount> GetProviderAccountById(Guid providerId)
		{
			var provider = await _context.ProviderAccount.AsNoTracking().FirstOrDefaultAsync(p =>p.ProviderId == providerId);
			if (provider == null)
			{
				return null;
			}
			else
			{
				return Mapper.MapProvider(provider);
			}
		}
		public void  AddNewProviderAccount(ProviderAccount newAccount)
		{
			var newEntity = Mapper.MapProvider(newAccount);
			_context.Add(newEntity);
		}
		public async Task UpdateProviderAccount(ProviderAccount providerAccount)
		{
			var oldEntity = await _context.ProviderAccount.FindAsync(providerAccount.ProviderId);
			var updatedEntity = Mapper.MapProvider(providerAccount);

			_context.Entry(oldEntity).CurrentValues.SetValues(updatedEntity);
		}
		public async Task DeleteProviderAccount(Guid providerId)
		{
			var entityToBeRemoved = await _context.ProviderAccount.FindAsync(providerId);
			_context.Remove(entityToBeRemoved);
		}	
		/* End */

		/* Coordinator Repos */
		public async Task<CoordinatorAccount> GetCoordinatorAccountById(Guid coordinatorId)
		{
			var coordinator = await _context.CoordinatorAccount.AsNoTracking().FirstOrDefaultAsync(p => p.CoordinatorId == coordinatorId);
			if (coordinator == null)
			{
				return null;
			}
			else
			{
				return Mapper.MapCoordinator(coordinator);
			}
		}
		/* End */

		/* Notification Repos */
		public async Task<Notification> GetNotificationById(Guid providerId)
		{
			var provider = await _context.ProviderAccount.AsNoTracking().FirstOrDefaultAsync(p => p.ProviderId == providerId);
			if (provider == null)
			{
				return null;
			}
			else
			{
				var notification = await _context.Notification.AsNoTracking().FirstOrDefaultAsync(p => p.ProviderId == providerId);
				return Mapper.MapNotification(notification);
			}
		}
		public void AddNewNotification(Notification newNofi)
		{
			var newEntity = Mapper.MapNotification(newNofi);
			_context.Add(newEntity);
		}
		public async Task DeleteNotificationById(Guid providerId)
		{
			var entityToBeRemoved = await _context.Notification.FindAsync(providerId);
			_context.Remove(entityToBeRemoved);
		}
		public async Task UpdateNotification(Notification notification)
		{
			var oldEntity = await _context.Notification.FindAsync(notification.ProviderId);
			var updatedEntity = Mapper.MapNotification(notification);

			_context.Entry(oldEntity).CurrentValues.SetValues(updatedEntity);
		}
		/* End */

		public async Task Save()
		{
			// TODO: Ideally put a log message here to notify when saving
			await _context.SaveChangesAsync();
		}
		public ValueTask DisposeAsync()
		{
			throw new NotImplementedException();
		}
	}
}
