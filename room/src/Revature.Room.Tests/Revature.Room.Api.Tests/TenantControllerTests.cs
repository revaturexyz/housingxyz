using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Revature.Room.Api.Controllers;
using Revature.Room.Lib;
using Xunit;

namespace Revature.Room.Tests.Revature.Room.DataAccess.Tests
{
  public class TenantControllerTests
  {
    /// <summary>
    /// Unit test for GetAsync method in TenantController
    /// </summary>
    [Fact]
    public async Task GetAsyncShouldReturnRoomList()
    {
      // arrange
      var mockRepo = new Mock<IRepository>();
      var mockLogger = new Mock<ILogger<TenantController>>();
      var controller = new TenantController(mockRepo.Object, mockLogger.Object);

      var gender = "nonbinary";
      var dateTime = new DateTime();

      // act
      var result = await controller.GetAsync(gender, dateTime);

      // assert
      Assert.NotNull(result);
      Assert.IsAssignableFrom<OkObjectResult>(result);
    }
  }
}
