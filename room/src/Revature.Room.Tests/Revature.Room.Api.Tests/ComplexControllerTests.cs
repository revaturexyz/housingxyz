using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Revature.Room.Api.Controllers;
using Revature.Room.Lib;
using ServiceBusMessaging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Revature.Room.DataAccess.Tests
{
  public class ComplexControllerTests
  {
    /// <summary>
    /// Test for Complex Controller method GetFilteredRooms
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task GetFilteredRoomsShouldFilterByComplexId()
    {
      //arrange
      var mockRepo = new Mock<IRepository>();
      var mockLogger = new Mock<ILogger>();
      //added this 
      var mockSender = new Mock<IServiceBusSender>();

      mockRepo.Setup(r => r.GetFilteredRoomsAsync(
        It.IsAny<Guid>(),
        It.IsAny<string>(),
        It.IsAny<int>(),
        It.IsAny<string>(),
        It.IsAny<string>(),
        It.IsAny<DateTime>(),
        It.IsAny<Guid>()))
        .Returns(Task.FromResult<IEnumerable<Lib.Room>>(
          new List<Lib.Room>()
          {
            new Lib.Room()
          }
        ));
      var controller = new ComplexController(mockRepo.Object, mockLogger.Object, mockSender.Object);

      //act
      var result = await controller.GetFilteredRoomsAsync(Guid.NewGuid(), "", 1, "", "", DateTime.Now, Guid.NewGuid());

      //assert
      Assert.IsAssignableFrom<OkObjectResult>(result);
    }

    /// <summary>
    /// Unit test for try/catch block for Complex Controller GetFilteredRoomsAsync.
    /// </summary>
    [Fact]
    public async Task GetFilteredRoomsAsyncShouldReturnKeyNotFoundException()
    {
      var mockRepo = new Mock<IRepository>();
      var mockLogger = new Mock<ILogger>();

      //added this
      var mockSender = new Mock<IServiceBusSender>();

      mockRepo.Setup(r => r.GetFilteredRoomsAsync(
        It.IsAny<Guid>(),
        It.IsAny<string>(),
        It.IsAny<int>(),
        It.IsAny<string>(),
        It.IsAny<string>(),
        It.IsAny<DateTime>(),
        It.IsAny<Guid>()))
        .Throws(new KeyNotFoundException());

      var controller = new ComplexController(mockRepo.Object, mockLogger.Object, mockSender.Object);

      var result = await controller.GetFilteredRoomsAsync(Guid.NewGuid(), "", 1, "", "", DateTime.Now, Guid.NewGuid());

      Assert.IsType<NotFoundObjectResult>(result);
    }
  }
}
