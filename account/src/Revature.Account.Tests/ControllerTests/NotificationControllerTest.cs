using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Revature.Account.Lib.Model;

namespace Revature.Account.Tests.ControllerTests
{
  /// <summary>
  /// Tests for the API's Notification Controller.
  /// </summary>
  public class NotifitcationControllerTest
  {
    /// <summary>
    /// Test for the retrieval of a Notification based on a coordinator account's Guid-Id. (GET)
    /// </summary>
    [Fact]
    public async Task GetNotificationsByCoordinatorIdAsync()
    {
      TestHelper helper = new TestHelper();
      Guid notificationId = helper.Notifications[0].NotificationId;
      Guid coordinatorId = helper.Notifications[0].CoordinatorId;

      helper.Repository
        .Setup(x => x.GetNotificationsByCoordinatorIdAsync(It.IsAny<Guid>()))
        .Returns(Task.Run(() => helper.Notifications.Where(n => n.NotificationId == notificationId).ToList()));

      Assert.NotNull(await helper.NotificationController.GetNotificationByCoordinatorIdAsync(coordinatorId) as OkObjectResult);
    }

    /// <summary>
    /// Test for the successful creation of a new Notification. (POST)
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task CreateNewNotificationSuccessfullyVerifyTestAsync()
    {
      TestHelper helper = new TestHelper();
      Guid coordinatorId = helper.Coordinators[0].CoordinatorId;
      Guid providerId = helper.Providers[0].ProviderId;
      var newNotification = new Notification();
      newNotification.ProviderId = providerId;
      newNotification.CoordinatorId = coordinatorId;
      newNotification.AccountExpiresAt = TestHelper.nowPSev;


      Guid newNotificationId = newNotification.NotificationId;


      helper.Repository
        .Setup(x => x.AddNotification(It.IsAny<Notification>()))
        .Verifiable();
      helper.Repository
        .Setup(x => x.GetStatusByStatusTextAsync(It.IsAny<string>()))
        .Returns(Task.FromResult(helper.Statuses[0]));

      var newNofi = await helper.NotificationController.Post(newNotification);

      helper.Repository
          .Verify();
    }

    /// <summary>
    /// Test for a successful Notification update (PUT).
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task UpdateNotificationAsyncSuccessfulVerificationAsync()
    {
      TestHelper helper = new TestHelper();
      Guid notificationId = helper.Notifications[0].NotificationId;
      var updatedNotification = helper.Notifications[0];
      updatedNotification.Status.StatusText = "Under Review";
      updatedNotification.AccountExpiresAt = new DateTime(2019, 11, 24, 3, 15, 0);

      helper.Repository
          .Setup(x => x.GetNotificationByIdAsync(It.IsAny<Guid>()))
              .Returns(Task.FromResult(helper.Notifications[0]))
              .Verifiable();
      helper.Repository
          .Setup(x => x.UpdateNotificationAsync(It.IsAny<Notification>()))
              .Returns(Task.FromResult(true))
              .Verifiable();

      var updatedResult = await helper.NotificationController.PUT(notificationId, updatedNotification.Status);

      helper.Repository
          .Verify();
    }

    /// <summary>
    /// Test for the sucessful deletion of a specified Notification. (DELETE)
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task DeleteNotificationAsyncSuccessfulVerificationAsync()
    {
      TestHelper helper = new TestHelper();
      Guid notificationId = helper.Notifications[0].NotificationId;

      helper.Repository
          .Setup(x => x.GetNotificationByIdAsync(It.IsAny<Guid>()))
              .Returns(Task.FromResult(helper.Notifications[0]))
              .Verifiable();
      helper.Repository
          .Setup(x => x.DeleteNotificationByIdAsync(It.IsAny<Guid>()))
              .Returns(Task.FromResult(true))
              .Verifiable();

      var deleted = await helper.NotificationController.Delete(notificationId);

      helper.Repository
          .Verify();
    }
  }
}
