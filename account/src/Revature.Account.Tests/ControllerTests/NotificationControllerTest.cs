using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Revature.Account.Tests.ControllerTests
{
  public class NotifitcationControllerTest
  {
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
  }
}
