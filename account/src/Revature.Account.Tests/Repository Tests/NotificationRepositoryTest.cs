using Microsoft.EntityFrameworkCore;
using Revature.Account.DataAccess;
using Revature.Account.DataAccess.Repositories;
using System;
using System.Linq;
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
    public async void GetNotificationByIdAsyncTest()
    {
      // Arrange
      TestHelper helper = new TestHelper();
      Mapper mapper = new Mapper();
      var testProvider = helper.IProviderAccountList[0];
      var testNotification = helper.INotificationList[0];

      var options = new DbContextOptionsBuilder<AccountDbContext>()
          .UseInMemoryDatabase("GetNotificationByProviderIdTest")
          .Options;
      using var arrangeContext = new AccountDbContext(options);
      arrangeContext.ProviderAccount.Add(mapper.MapProvider(testProvider));
      arrangeContext.CoordinatorAccount.Add(mapper.MapCoordinator(helper.ICoordinators[0]));
      arrangeContext.Notification.Add(mapper.MapNotification(testNotification));
      arrangeContext.SaveChanges();
      var testId = testNotification.NotificationId;
      using var actContext = new AccountDbContext(options);
      var repo = new GenericRepository(actContext);
      // Act
      var result = await repo.GetNotificationByIdAsync(testId);
      // Assert
      Assert.Equal(testId, result.NotificationId);
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
        AccountExpiresAt = DateTime.Now.AddDays(7)
      };
      var actRepo = new GenericRepository(actContext);
      // Act
      actRepo.AddNotification(newNotification);
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
        AccountExpiresAt = DateTime.Now.AddDays(7)
      };
      arrangeContext.Notification.Add(arrangeNotification);
      var updatedNotification = new Lib.Model.Notification
      {
        NotificationId = notificationId,
        ProviderId = providerId,
        CoordinatorId = coordinatorId,
        Status = updatedStatus,
        AccountExpiresAt = DateTime.Now.AddDays(30)
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
        AccountExpiresAt = DateTime.Now.AddDays(7)
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
