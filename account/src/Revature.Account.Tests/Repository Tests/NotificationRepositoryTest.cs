using Microsoft.EntityFrameworkCore;
using Revature.Account.DataAccess;
using Revature.Account.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Revature.Account.Tests.Repository_Tests
{
  public class NotificationRepositoryTest
  {
    public Guid coordinatorId = Guid.NewGuid();
    public Guid providerId = Guid.NewGuid();
    public Guid notificationId = Guid.NewGuid();
    [Fact]
    public async void GetNotificationByProviderIdTest()
    {
      // Arrange
      var options = new DbContextOptionsBuilder<AccountDbContext>()
          .UseInMemoryDatabase("GetNotificationByProviderIdTest")
          .Options;
      using var arrangeContext = new AccountDbContext(options);
      var testId = providerId;
      var testProviderEntity = new DataAccess.Entities.ProviderAccount
      {
        CoordinatorId = coordinatorId,
        ProviderId = providerId,
        Name = "Joe Doe",
        Password = "Abcxzy123",
        Status = "Pending",
        AccountCreated = DateTime.Now,
        Expire = DateTime.Now.AddDays(7),
      };
      arrangeContext.ProviderAccount.Add(testProviderEntity);
      var testNotificationEntity = new DataAccess.Entities.Notification
      {
        CoordinatorId = coordinatorId,
        ProviderId = providerId,
        Status = "Pending",
        AccountExpire = DateTime.Now.AddDays(7),
        NotificationId = notificationId
      };
      arrangeContext.Notification.Add(testNotificationEntity);
      arrangeContext.SaveChanges();
      using var actContext = new AccountDbContext(options);
      var repo = new GenericRepository(actContext);
      // Act
      var result = await repo.GetNotificationByIdAsync(testId);
      // Assert
      Assert.Equal(testId, result.ProviderId);
    }
    [Fact]
    public void AddNewNotificationTest()
    {
      // Arrange
      var options = new DbContextOptionsBuilder<AccountDbContext>()
          .UseInMemoryDatabase("AddNewNotificationTest")
          .Options;
      using var actContext = new AccountDbContext(options);
      using var arrangeContext = new AccountDbContext(options);
      var newNotification = new Lib.Model.Notification
      {
        NotificationId = notificationId,
        ProviderId = providerId,
        CoordinatorId = coordinatorId,
        Status = "Pending",
        AccountExpire = DateTime.Now.AddDays(7)
      };
      var actRepo = new GenericRepository(actContext);
      // Act
      actRepo.AddNewNotification(newNotification);
      actContext.SaveChanges();
      // Assert
      using var assertContext = new AccountDbContext(options);
      var assertNotification = assertContext.Notification.First(p => p.CoordinatorId == newNotification.CoordinatorId);
      Assert.NotNull(assertNotification);
    }
    [Fact]
    public async Task UpdateNotificationAccountTestAsync()
    {
      // Arrange
      var updatedStatus = "Under Review";
      var options = new DbContextOptionsBuilder<AccountDbContext>()
          .UseInMemoryDatabase("UpdateNotificationAccountTestAsync")
          .Options;
      using var arrangeContext = new AccountDbContext(options);
      var arrangeNotification = new DataAccess.Entities.Notification
      {
        NotificationId = notificationId,
        ProviderId = providerId,
        CoordinatorId = coordinatorId,
        Status = "Pending",
        AccountExpire = DateTime.Now.AddDays(7)
      };
      arrangeContext.Notification.Add(arrangeNotification);
      var updatedNotification = new Lib.Model.Notification
      {
        NotificationId = notificationId,
        ProviderId = providerId,
        CoordinatorId = coordinatorId,
        Status = updatedStatus,
        AccountExpire = DateTime.Now.AddDays(30)
      };
      // Act
      var repo = new GenericRepository(arrangeContext);
      await repo.UpdateNotificationAsync(updatedNotification);
      arrangeContext.SaveChanges();
      // Assert
      var assertContext = new AccountDbContext(options);
      var assertNotification = assertContext.Notification.First(p => p.CoordinatorId == coordinatorId);
      Assert.Equal(updatedStatus, assertNotification.Status);
    }
    [Fact]
    public async Task DeleteNotificationTestAsync()
    {
      //Assemble
      var options = new DbContextOptionsBuilder<AccountDbContext>()
          .UseInMemoryDatabase("DeleteNotificationTestAsync")
          .Options;
      using var assembleContext = new AccountDbContext(options);
      var deleteNotification = new DataAccess.Entities.Notification
      {
        ProviderId = providerId,
        CoordinatorId = coordinatorId,
        NotificationId = notificationId,
        Status = "Pending",
        AccountExpire = DateTime.Now.AddDays(7)
      };
      assembleContext.Add(deleteNotification);
      assembleContext.SaveChanges();
      using var actContext = new AccountDbContext(options);
      var repo = new GenericRepository(actContext);
      // Act
      await repo.DeleteNotificationByIdAsync(notificationId);
      // Assert
      var notification = actContext.Notification.ToList();
      Assert.DoesNotContain(deleteNotification, notification);
    }
  }
}
