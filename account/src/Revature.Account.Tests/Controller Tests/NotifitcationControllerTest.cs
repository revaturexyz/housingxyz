using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Revature.Account.DataAccess.Entities;

namespace Revature.Account.Tests.Controller_Tests
{
  public class NotifitcationControllerTest
  {
    [Fact]
    public async Task GetNotificationsByCoordinatorIdAsync()
    {
      TestHelper helper = new TestHelper();
      Guid notificationId = helper.Notifications[0].NotificationId;
      Guid coordinatorId = helper.Notifications[0].CoordinatorId;
    }
  }
}
