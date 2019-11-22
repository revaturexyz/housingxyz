using Microsoft.EntityFrameworkCore;
using Revature.Account.DataAccess;
using Revature.Account.DataAccess.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Revature.Account.Tests.Repository_Tests
{
  /// <summary>
  /// Tests for a Notification's data-access-layer and it's supporting database-negotiation methods.
  /// </summary>
  public class NotificationRepositoryTest
  {
    /// <summary>
    /// Retrieve a notification from the database by way of a given Guid-Id.
    /// </summary>
    [Fact]
    public async void GetNotificationByIdAsyncTest()
    {
      // Arrange
      TestHelper helper = new TestHelper();
      Mapper mapper = new Mapper();
      var testProvider = helper.Providers[0];
      var testNotification = helper.Notifications[0];

      var options = new DbContextOptionsBuilder<AccountDbContext>()
          .UseInMemoryDatabase("GetNotificationByProviderIdTest")
          .Options;
      using var arrangeContext = new AccountDbContext(options);
      arrangeContext.ProviderAccount.Add(mapper.MapProvider(testProvider));
      arrangeContext.CoordinatorAccount.Add(mapper.MapCoordinator(helper.Coordinators[0]));
      arrangeContext.Notification.Add(mapper.MapNotification(testNotification));
      arrangeContext.Status.Add(mapper.MapStatus(helper.Statuses[0]));
      arrangeContext.SaveChanges();
      var testId = testNotification.NotificationId;
      using var actContext = new AccountDbContext(options);
      var repo = new GenericRepository(actContext);
      // Act
      var result = await repo.GetNotificationByIdAsync(testId);
      // Assert
      Assert.Equal(testId, result.NotificationId);
    }

    /// <summary>
    /// Test for adding a new notification to the database.
    /// </summary>
    [Fact]
    public void AddNewNotificationTest()
    {
      // Arrange
      TestHelper helper = new TestHelper();
      Mapper mapper = new Mapper();
      var options = new DbContextOptionsBuilder<AccountDbContext>()
          .UseInMemoryDatabase("AddNewNotificationTest")
          .Options;
      using var actContext = new AccountDbContext(options);
      using var arrangeContext = new AccountDbContext(options);
      var actRepo = new GenericRepository(actContext);
      var newNotification = helper.Notifications[0];
      
      // Act
      actRepo.AddNotification(newNotification);
      actContext.SaveChanges();

      // Assert
      using var assertContext = new AccountDbContext(options);
      var assertNotification = assertContext.Notification.First(p => p.CoordinatorId == newNotification.CoordinatorId);
      Assert.NotNull(assertNotification);
    }

    /// <summary>
    /// Update a given notification's entry in the database.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task UpdateNotificationAccountTestAsync()
    {
      // Arrange
      TestHelper helper = new TestHelper();
      Mapper mapper = new Mapper();
      var options = new DbContextOptionsBuilder<AccountDbContext>()
          .UseInMemoryDatabase("UpdateNotificationAccountTestAsync")
          .Options;
      using var arrangeContext = new AccountDbContext(options);
      var arrangeNotification = helper.Notifications[0];
      arrangeContext.Notification.Add(mapper.MapNotification(arrangeNotification));
      var repo = new GenericRepository(arrangeContext);

      // Act
      arrangeNotification.CoordinatorId = helper.Coordinators[1].CoordinatorId;
      await repo.UpdateNotificationAsync(arrangeNotification);
      arrangeContext.SaveChanges();

      // Assert
      var assertContext = new AccountDbContext(options);
      var assertNotification = assertContext.Notification.First(p => p.CoordinatorId == arrangeNotification.CoordinatorId);
      Assert.Equal(arrangeNotification.CoordinatorId, assertNotification.CoordinatorId);
    }

    /// <summary>
    /// Delete a notification's entry from the database.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task DeleteNotificationTestAsync()
    {
      //Assemble
      TestHelper helper = new TestHelper();
      Mapper mapper = new Mapper();
      var options = new DbContextOptionsBuilder<AccountDbContext>()
          .UseInMemoryDatabase("DeleteNotificationTestAsync")
          .Options;
      using var assembleContext = new AccountDbContext(options);
      var deleteNotification = mapper.MapNotification(helper.Notifications[0]);
      assembleContext.Add(deleteNotification);
      assembleContext.SaveChanges();
      using var actContext = new AccountDbContext(options);
      var repo = new GenericRepository(actContext);

      // Act
      await repo.DeleteNotificationByIdAsync(deleteNotification.NotificationId);

      // Assert
      var notification = actContext.Notification.ToList();
      Assert.DoesNotContain(deleteNotification, notification);
    }
  }
}
