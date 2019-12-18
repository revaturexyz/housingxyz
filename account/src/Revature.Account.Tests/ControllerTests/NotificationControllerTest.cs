using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Revature.Account.Lib.Model;
using Xunit;

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

      Assert.NotNull(await helper.NotificationController.GetNotificationsByCoordinatorIdAsync(coordinatorId) as OkObjectResult);
    }

    /// <summary>
    /// Test for the successful creation of a new Notification. (POST)
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task CreateNewNotificationSuccessfullyVerifyTestAsync()
    {
      TestHelper helper = new TestHelper();

      helper.Repository
        .Setup(x => x.AddNotification(It.IsAny<Notification>()))
        .Verifiable();

      var newNofi = await helper.NotificationController.Post(helper.Notifications[0]);

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
      updatedNotification.Status.StatusText = Status.UnderReview;
      updatedNotification.CreatedAt = new DateTime(2019, 11, 24, 3, 15, 0);

      helper.Repository
          .Setup(x => x.GetNotificationByIdAsync(It.IsAny<Guid>()))
              .Returns(Task.FromResult(helper.Notifications[0]))
              .Verifiable();
      helper.Repository
          .Setup(x => x.UpdateNotificationAsync(It.IsAny<Notification>()))
              .Returns(Task.FromResult(true))
              .Verifiable();

      var updatedResult = await helper.NotificationController.Put(updatedNotification.CoordinatorId, updatedNotification.Status.StatusText);

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
