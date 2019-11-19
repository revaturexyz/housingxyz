using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Revature.Account.Lib.Interface;
using Revature.Account.Lib.Model;

namespace Revature.Account.DataAccess.Repositories
{
  public class GenericRepository : IGenericRepository
  {
    private readonly AccountDbContext _context;
    private readonly Mapper mapper;

    public GenericRepository(AccountDbContext db)
    {
      _context = db ?? throw new ArgumentNullException("Context cannot be null.", nameof(db));
      this.mapper = new Mapper();
    }
    /* Provider Repos */
    public async Task<ProviderAccount> GetProviderAccountByIdAsync(Guid providerId)
    {
      var provider = await _context.ProviderAccount.AsNoTracking().FirstOrDefaultAsync(p => p.ProviderId == providerId);
      if (provider == null)
      {
        return null;
      }
      else
      {
        return mapper.MapProvider(provider);
      }
    }
    public void AddNewProviderAccountAsync(ProviderAccount newAccount)
    {
      var newEntity = mapper.MapProvider(newAccount);
      _context.Add(newEntity);
    }
    public async Task UpdateProviderAccountAsync(ProviderAccount providerAccount)
    {
      var existingEntity = await _context.ProviderAccount.FindAsync(providerAccount.ProviderId);
      var updatedEntity = mapper.MapProvider(providerAccount);
      _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
    }
    public async Task DeleteProviderAccountAsync(Guid providerId)
    {
      var entityToBeRemoved = await _context.ProviderAccount.FindAsync(providerId);
      _context.Remove(entityToBeRemoved);
    }
    /* End */
    /* Coordinator Repos */
    public async Task<CoordinatorAccount> GetCoordinatorAccountByIdAsync(Guid coordinatorId)
    {
      var coordinator = await _context.CoordinatorAccount.AsNoTracking().FirstOrDefaultAsync(p => p.CoordinatorId == coordinatorId);
      if (coordinator == null)
      {
        return null;
      }
      else
      {
        return mapper.MapCoordinator(coordinator);
      }
    }
    /* End */
    /* Notification Repos */
    public async Task<Notification> GetNotificationByIdAsync(Guid providerId)
    {
      var provider = await _context.ProviderAccount.AsNoTracking().FirstOrDefaultAsync(p => p.ProviderId == providerId);
      if (provider == null)
      {
        return null;
      }
      else
      {
        var notification = await _context.Notification.AsNoTracking().FirstOrDefaultAsync(p => p.ProviderId == providerId);
        return mapper.MapNotification(notification);
      }
    }
    public void AddNewNotification(Notification newNofi)
    {
      var newEntity = mapper.MapNotification(newNofi);
      _context.Add(newEntity);
    }
    public async Task DeleteNotificationByIdAsync(Guid providerId)
    {
      var entityToBeRemoved = await _context.Notification.FindAsync(providerId);
      _context.Remove(entityToBeRemoved);
    }
    public async Task UpdateNotificationAsync(Notification notification)
    {
      var existingEntity = await _context.Notification.FindAsync(notification.NotificationId);
      var updatedEntity = mapper.MapNotification(notification);

      _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
    }
    /* End */
    public async Task SaveAsync()
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
