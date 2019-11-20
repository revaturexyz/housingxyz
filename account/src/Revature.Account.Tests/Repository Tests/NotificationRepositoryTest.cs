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
