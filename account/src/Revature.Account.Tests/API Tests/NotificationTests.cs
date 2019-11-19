using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

using Xunit;
using Moq;

using Revature.Account.Api;

namespace Revature.Account.Test.API_Tests
{
  public class NotificationTests
  {
    [Fact]
    public async Task GetSingleNotificationHasCorrectProviderId()
    {
      //get the helper
      ControllerTestHelper helper = new ControllerTestHelper();

      //get the guid for the target to be tested
      Guid targetId = helper.INotificationList[0].ProviderId;

      //look in the repository
      helper.IRepository
          //get the notification: is Identified by the Provider's id number.
          .Setup(x => x.GetNotificationById(It.IsAny<Guid>()))
          //get the notification in question.
          .Returns(Task.Run(() => helper.INotificationList.Where(c => c.ProviderId == targetId).FirstOrDefault()));

      
      //determine if null, and say so.
      Assert.NotNull(( await helper.INotificationController.GetNotificationByProviderId( targetId ) as OkObjectResult ).Value );

    }

  }
}
