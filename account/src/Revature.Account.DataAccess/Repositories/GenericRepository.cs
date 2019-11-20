using Microsoft.EntityFrameworkCore;
using Revature.Account.Lib.Interface;
using Revature.Account.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace Revature.Account.DataAccess.Repositories
{
  public class GenericRepository : IGenericRepository
  {
    private readonly AccountDbContext _context;
    private readonly Mapper mapper;
    private readonly ILogger _logger;

    public GenericRepository(AccountDbContext db, ILogger logger)
    {
      _context = db ?? throw new ArgumentNullException("Context cannot be null.", nameof(db));
      this.mapper = new Mapper();
      this._logger = logger ?? throw new ArgumentNullException(nameof(logger));

    }

    #region Provider

    public async Task<ProviderAccount> GetProviderAccountByIdAsync(Guid providerId)
    {
      try
      {
        _logger.Information("Getting Provider by ID: {providerId}", providerId);
        var provider = await _context.ProviderAccount
        .AsNoTracking()
        .Include(p => p.Coordinator)
        .Include(p => p.Status)
        .FirstOrDefaultAsync(p => p.ProviderId == providerId);
        return (provider != null ? mapper.MapProvider(provider) : null);
      }
      catch(InvalidOperationException e)
      {
        _logger.Error("Invalid Operation Exception Error. Error message: " +  e.Message);
        return null;
      }
      catch(Exception e)
      {
        _logger.Error("Something went wrong when getting a provider account by ID. Error: " + e.Message);
        return null;
      }
      
    }

    public void AddProviderAccountAsync(ProviderAccount newAccount)
    {
      try
      {
        _logger.Information("Adding provider account for {name}", newAccount.Name);
        var newEntity = mapper.MapProvider(newAccount);
        _context.Add(newEntity);
      }
      catch(InvalidOperationException e)
      {
        _logger.Error("Invalid Operation Exception Error. Error message: " + e.Message);
      }
      catch(Exception e)
      {
        _logger.Error("Something went wrong adding a provider account. Error message: " + e.Message);
      }
    }
    public async Task<bool> UpdateProviderAccountAsync(ProviderAccount providerAccount)
    {
      try
      {
        _logger.Information("Updating providerAccount for {Name}", providerAccount.Name);
        var existingEntity = await _context.ProviderAccount.FindAsync(providerAccount.ProviderId);
        if (existingEntity == null)
        {
          return false;
        }
        var updatedEntity = mapper.MapProvider(providerAccount);
        _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
        return true;
      }
      catch (InvalidOperationException e)
      {
        _logger.Error("Invalid Operation Exception Error. Error message: " + e.Message);
        throw new InvalidOperationException(e.Message);
      }
      catch (Exception e)
      {
        _logger.Error("Something went wrong updating provider account. Error message: " + e.Message);
        throw new Exception(e.Message);
      }
    }

    public async Task<bool> DeleteProviderAccountAsync(Guid providerId)
    {
      try
      {
        _logger.Information("Deleting provider account for given ID: {providerId}", providerId);
        var entityToBeRemoved = await _context.ProviderAccount.FindAsync(providerId);
        if (entityToBeRemoved == null)
        {
          return false;
        }
        _context.Remove(entityToBeRemoved);
        return true;
      }
      catch(InvalidOperationException e)
      {
        _logger.Error("Invalid Operation Exception Error. Error message: " + e.Message);
        throw new InvalidOperationException(e.Message);
      }
      catch(Exception e)
      {
        _logger.Error("Something went wrong deleting a provier account. Error message: " + e.Message);
        throw new Exception(e.Message);
      }
      
    }

    #endregion

    #region Coordinator

    public async Task<CoordinatorAccount> GetCoordinatorAccountByIdAsync(Guid coordinatorId)
    {
      try
      {
        var coordinator = await _context.CoordinatorAccount
        .AsNoTracking()
        .Include(c => c.Notifications)
        .FirstOrDefaultAsync(p => p.CoordinatorId == coordinatorId);
        return (coordinator != null ? mapper.MapCoordinator(coordinator) : null);
      }
      catch(InvalidOperationException e)
      {

      }
      catch(Exception e)
      {

      }
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
      var notification = _context.Notification
        .Include(n => n.Coordinator)
        .Include(n => n.Provider)
        .Include(n => n.Status)
        .Where(p => p.CoordinatorId == coordinatorId);
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
      // TODO: Ideally put a log message here to notify when saving
      await _context.SaveChangesAsync();
    }
  }
}
