using Microsoft.EntityFrameworkCore;
using Revature.Account.Lib.Interface;
using Revature.Account.Lib.Model;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

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
      return (provider != null ? mapper.MapProvider(provider) : null);
    }
    public void AddNewProviderAccountAsync(ProviderAccount newAccount)
    {
      var newEntity = mapper.MapProvider(newAccount);
      _context.Add(newEntity);
    }
    public async Task<bool> UpdateProviderAccountAsync(ProviderAccount providerAccount)
    {
      var existingEntity = await _context.ProviderAccount.FindAsync(providerAccount.ProviderId);
      if (existingEntity == null)
        return false;

      var updatedEntity = mapper.MapProvider(providerAccount);
      _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
      return true;
    }
    public async Task<bool> DeleteProviderAccountAsync(Guid providerId)
    {
      var entityToBeRemoved = await _context.ProviderAccount.FindAsync(providerId);
      if (entityToBeRemoved == null)
        return false;

      _context.Remove(entityToBeRemoved);
      return true;
    }
    /* End */
    /* Coordinator Repos */
    public async Task<CoordinatorAccount> GetCoordinatorAccountByIdAsync(Guid coordinatorId)
    {
      var coordinator = await _context.CoordinatorAccount.AsNoTracking().FirstOrDefaultAsync(p => p.CoordinatorId == coordinatorId);
      return (coordinator != null ? mapper.MapCoordinator(coordinator) : null);
    }
    /* End */
    /* Notification Repos */
    public async Task<Notification> GetNotificationByIdAsync(Guid notificationId)
    {
      var notification = await _context.Notification.AsNoTracking().FirstOrDefaultAsync(p => p.NotificationId == notificationId);
      return (notification != null ? mapper.MapNotification(notification) : null);
    }
    public async Task<List<Notification>> GetNotificationsByCoordinatorIdAsync(Guid coordinatorId)
    {
      var notification = _context.Notification.Where(p => p.CoordinatorId == coordinatorId);
      return (notification != null ? notification.Select(mapper.MapNotification).ToList() : null);
    }
    public void AddNewNotification(Notification newNofi)
    {
      var newEntity = mapper.MapNotification(newNofi);
      _context.Add(newEntity);
    }
    public async Task<bool> DeleteNotificationByIdAsync(Guid notificationId)
    {
      var entityToBeRemoved = await _context.Notification.FindAsync(notificationId);
      if (entityToBeRemoved == null)
        return false;

      _context.Remove(entityToBeRemoved);
      return true;
    }
    public async Task<bool> UpdateNotificationAsync(Notification notification)
    {
      var existingEntity = await _context.Notification.FindAsync(notification.NotificationId);
      if (existingEntity == null)
        return false;

      var updatedEntity = mapper.MapNotification(notification);
      _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
      return true;
    }
    /* End */
    public async Task SaveAsync()
    {
      // TODO: Ideally put a log message here to notify when saving
      await _context.SaveChangesAsync();
    }
  }
}
