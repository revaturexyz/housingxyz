using Microsoft.EntityFrameworkCore;
using Revature.Account.Lib.Interface;
using Revature.Account.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revature.Account.DataAccess.Repositories
{
  /// <summary>
  /// Describes the methods used to get and manipulate data between the database and business layer.
  /// </summary>
  public class GenericRepository : IGenericRepository
  {

    //the database
    private readonly AccountDbContext _context;

    //the mapper tool
    private readonly Mapper mapper;

    //constructor
    public GenericRepository(AccountDbContext db)
    {
      //inject the database
      _context = db ?? throw new ArgumentNullException(nameof(db), "Context cannot be null.");

      //instantiate the mapper
      this.mapper = new Mapper();
    }

    #region Provider

    public async Task<Guid> GetProviderIdByEmailAsync(string providerEmail)
    {
      var provider = await _context.ProviderAccount
        .AsNoTracking()
        .FirstOrDefaultAsync(p => p.Email == providerEmail);
      return (provider != null ? provider.ProviderId : Guid.Empty);
    }

    /// <summary>
    /// Get the Provider by Guid
    /// </summary>
    /// <param name="providerId"></param>
    /// <returns></returns>
    public async Task<ProviderAccount> GetProviderAccountByIdAsync(Guid providerId)
    {
      var provider = await _context.ProviderAccount
        .AsNoTracking()
        .Include(p => p.Coordinator)
        .FirstOrDefaultAsync(p => p.ProviderId == providerId);
      return (provider != null ? mapper.MapProvider(provider) : null);
    }

    /// <summary>
    /// Add a Provider to the database.
    /// </summary>
    /// <param name="newAccount"></param>
    public void AddProviderAccountAsync(ProviderAccount newAccount)
    {
      var newEntity = mapper.MapProvider(newAccount);
      _context.Add(newEntity);
    }

    /// <summary>
    /// Update a Provider's account on the database.
    /// </summary>
    /// <param name="providerAccount"></param>
    /// <returns></returns>
    public async Task<bool> UpdateProviderAccountAsync(ProviderAccount providerAccount)
    {
      var existingEntity = await _context.ProviderAccount.FirstOrDefaultAsync(p => p.ProviderId == providerAccount.ProviderId);
      if (existingEntity == null)
        return false;

      var updatedEntity = mapper.MapProvider(providerAccount);
      _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
      return true;
    }

    /// <summary>
    /// Delete a provider's account from the database.
    /// </summary>
    /// <param name="providerId"></param>
    /// <returns></returns>
    public async Task<bool> DeleteProviderAccountAsync(Guid providerId)
    {
      var entityToBeRemoved = await _context.ProviderAccount.FirstOrDefaultAsync(p => p.ProviderId == providerId);
      if (entityToBeRemoved == null)
        return false;

      _context.Remove(entityToBeRemoved);
      return true;
    }

    #endregion

    #region Coordinator

    public async Task<Guid> GetCoordinatorIdByEmailAsync(string coordinatorEmail)
    {
      var coordinator = await _context.CoordinatorAccount
        .AsNoTracking()
        .FirstOrDefaultAsync(c => c.Email == coordinatorEmail);
      return (coordinator != null ? coordinator.CoordinatorId : Guid.Empty);
    }

    /// <summary>
    /// Get a Coordinator's account from the databse.
    /// </summary>
    /// <param name="coordinatorId"></param>
    /// <returns></returns>
    public async Task<CoordinatorAccount> GetCoordinatorAccountByIdAsync(Guid coordinatorId)
    {
      var coordinator = await _context.CoordinatorAccount
        .AsNoTracking()
        .Include(c => c.Notifications)
        .FirstOrDefaultAsync(p => p.CoordinatorId == coordinatorId);
      return (coordinator != null ? mapper.MapCoordinator(coordinator) : null);
    }

    public void AddCoordinatorAccount(CoordinatorAccount coordinator)
    {
      var newEntity = mapper.MapCoordinator(coordinator);
      _context.Add(newEntity);
    }

    public async Task<bool> UpdateCoordinatorAccountAsync(CoordinatorAccount coordinator)
    {
      var existingEntity = await _context.CoordinatorAccount.FindAsync(coordinator.CoordinatorId);
      if (existingEntity == null)
        return false;

      var updatedEntity = mapper.MapCoordinator(coordinator);
      _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
      return true;
    }

    public async Task<bool> DeleteCoordinatorAccountAsync(Guid coordinatorId)
    {
      var entityToBeRemoved = await _context.CoordinatorAccount.FindAsync(coordinatorId);
      if (entityToBeRemoved == null)
        return false;

      _context.Remove(entityToBeRemoved);
      return true;
    }

    #endregion

    #region Notification

    /// <summary>
    /// Get a notification by it's Guid-Id.
    /// </summary>
    /// <param name="notificationId"></param>
    /// <returns></returns>
    public async Task<Notification> GetNotificationByIdAsync(Guid notificationId)
    {
      var notification = await _context.Notification
        .AsNoTracking()
        .Include(n => n.Coordinator)
        .Include(n => n.Provider)
        .Include(n => n.UpdateAction)
        .FirstOrDefaultAsync(n => n.NotificationId == notificationId);
      return (notification != null ? mapper.MapNotification(notification) : null);
    }

    /// <summary>
    /// Get a list of notifications based on the attached coordinator.
    /// </summary>
    /// <param name="coordinatorId"></param>
    /// <returns></returns>
    public async Task<List<Notification>> GetNotificationsByCoordinatorIdAsync(Guid coordinatorId)
    {
      var notification = await _context.Notification
        .Include(n => n.Coordinator)
        .Include(n => n.Provider)
        .Include(n => n.UpdateAction)
        .Where(p => p.CoordinatorId == coordinatorId).ToListAsync();
      return notification?.Select(mapper.MapNotification).ToList();
    }

    /// <summary>
    /// Ad a new notification to the database    /// </summary>
    /// <param name="newNofi"></param>
    public void AddNotification(Notification newNofi)
    {
      newNofi.UpdateAction.NotificationId = newNofi.NotificationId;
      AddUpdateAction(newNofi.UpdateAction);

      var addedNotifiaction = mapper.MapNotification(newNofi);
      _context.Add(addedNotifiaction);
    }

    /// <summary>
    /// Delete an individual notification from the database.
    /// </summary>
    /// <param name="notificationId"></param>
    /// <returns></returns>
    public async Task<bool> DeleteNotificationByIdAsync(Guid notificationId)
    {
      var entityToBeRemoved = await _context.Notification.FirstOrDefaultAsync(n => n.NotificationId == notificationId);
      if (entityToBeRemoved == null)
        return false;

      if (await DeleteUpdateActionByIdAsync(entityToBeRemoved.UpdateActionId) == false)
        return false;

      _context.Remove(entityToBeRemoved);
      return true;
    }

    /// <summary>
    /// Update an individual notification stored on the database.
    /// </summary>
    /// <param name="notification"></param>
    /// <returns></returns>
    public async Task<bool> UpdateNotificationAsync(Notification notification)
    {
      var existingEntity = await _context.Notification.FirstOrDefaultAsync(n => n.NotificationId == notification.NotificationId);
      if (existingEntity == null)
        return false;

      if (await UpdateUpdateActionAsync(notification.UpdateAction) == false)
        return false;

      var updatedEntity = mapper.MapNotification(notification);
      _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
      return true;
    }

    #endregion

    #region UpdateAction

    public async Task<UpdateAction> GetUpdateActionByIdAsync(Guid actionId)
    {
      var action = await _context.UpdateAction
        .AsNoTracking()
        .FirstOrDefaultAsync(u => u.UpdateActionId == actionId);
      return (action != null ? mapper.MapUpdateAction(action) : null);
    }

    public void AddUpdateAction(UpdateAction action)
    {
      var newEntity = mapper.MapUpdateAction(action);
      _context.Add(newEntity);
    }

    public async Task<bool> UpdateUpdateActionAsync(UpdateAction action)
    {
      var existingEntity = await _context.UpdateAction.FirstOrDefaultAsync(u => u.UpdateActionId == action.UpdateActionId);
      if (existingEntity == null)
        return false;

      var updatedEntity = mapper.MapUpdateAction(action);
      _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
      return true;
    }

    public async Task<bool> DeleteUpdateActionByIdAsync(Guid actionId)
    {
      var entityToBeRemoved = await _context.UpdateAction.FirstOrDefaultAsync(u => u.UpdateActionId == actionId);
      if (entityToBeRemoved == null)
        return false;

      _context.Remove(entityToBeRemoved);
      return true;
    }
    #endregion

    /// <summary>
    /// Save changes in context to the database.
    /// </summary>
    public async Task SaveAsync()
    {
      await _context.SaveChangesAsync();
    }
  }
}
