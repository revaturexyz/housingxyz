using Microsoft.EntityFrameworkCore;
using Revature.Account.Lib.Interface;
using Revature.Account.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revature.Account.DataAccess.Repositories
{
  public class GenericRepository : IGenericRepository
  {
    private readonly AccountDbContext _context;
    private readonly Mapper mapper;

    public GenericRepository(AccountDbContext db)
    {
      _context = db ?? throw new ArgumentNullException(nameof(db), "Context cannot be null.");
      this.mapper = new Mapper();
    }

    #region Provider

    public async Task<ProviderAccount> GetProviderAccountByIdAsync(Guid providerId)
    {
      var provider = await _context.ProviderAccount
        .AsNoTracking()
        .Include(p => p.Coordinator)
        .Include(p => p.Status)
        .FirstOrDefaultAsync(p => p.ProviderId == providerId);
      return (provider != null ? mapper.MapProvider(provider) : null);
    }

    public void AddProviderAccountAsync(ProviderAccount newAccount)
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

    #endregion

    #region Coordinator

    public async Task<CoordinatorAccount> GetCoordinatorAccountByIdAsync(Guid coordinatorId)
    {
      var coordinator = await _context.CoordinatorAccount
        .AsNoTracking()
        .Include(c => c.Notifications)
        .FirstOrDefaultAsync(p => p.CoordinatorId == coordinatorId);
      return (coordinator != null ? mapper.MapCoordinator(coordinator) : null);
    }

    #endregion

    #region Notification

    public async Task<Notification> GetNotificationByIdAsync(Guid notificationId)
    {
      var notification = await _context.Notification
        .AsNoTracking()
        .Include(n => n.Coordinator)
        .Include(n => n.Provider)
        .Include(n => n.Status)
        .FirstOrDefaultAsync(p => p.NotificationId == notificationId);
      return (notification != null ? mapper.MapNotification(notification) : null);
    }

    public async Task<List<Notification>> GetNotificationsByCoordinatorIdAsync(Guid coordinatorId)
    {
      var notification = await _context.Notification
        .Include(n => n.Coordinator)
        .Include(n => n.Provider)
        .Include(n => n.Status)
        .Where(p => p.CoordinatorId == coordinatorId).ToListAsync();
      return (notification != null ? notification.Select(mapper.MapNotification).ToList() : null);
    }

    public void AddNotification(Notification newNofi)
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

    #endregion

    #region Status

    public async Task<Status> GetStatusByIdAsync(int statusId)
    {
      var status = await _context.Status
        .AsNoTracking()
        .FirstOrDefaultAsync(s => s.StatusId == statusId);
      return (status != null ? mapper.MapStatus(status) : null);
    }

    public async Task<Status> GetStatusByStatusTextAsync(string statusText)
    {
      var status = await _context.Status
        .AsNoTracking()
        .FirstOrDefaultAsync(s => s.StatusText == statusText);
      return (status != null ? mapper.MapStatus(status) : null);
    }

    public void AddStatus(Status status)
    {
      var newEntity = mapper.MapStatus(status);
      _context.Add(newEntity);
    }

    public async Task<bool> UpdateStatusAsync(Status status)
    {
      var existingEntity = await _context.Status.FindAsync(status.StatusId);
      if (existingEntity == null)
        return false;

      var updatedEntity = mapper.MapStatus(status);
      _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
      return true;
    }

    public async Task<bool> DeleteStatusByIdAsync(int statusId)
    {
      var entityToBeRemoved = await _context.Status.FindAsync(statusId);
      if (entityToBeRemoved == null)
        return false;

      _context.Remove(entityToBeRemoved);
      return true;
    }
    #endregion

    public async Task SaveAsync()
    {
      await _context.SaveChangesAsync();
    }
  }
}
