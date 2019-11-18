using Microsoft.AspNetCore.Mvc;
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
  public class RoomControllerTests
  {
    [Fact]
    public async Task GetFilteredRoomsShouldFilterByComplexId()
    {
      //arrange
      var mockRepo = new Mock<IRepository>();
      var mockServiceBus = new Mock<IServiceBusSender>();

      mockRepo.Setup<Task<IEnumerable<Lib.Room>>>(r => r.GetFilteredRoomsAsync(
        It.IsAny<Guid>(),
        It.IsAny<string>(),
        It.IsAny<int>(),
        It.IsAny<string>(),
        It.IsAny<string>(),
        It.IsAny<DateTime>()))
        .Returns(Task.FromResult<IEnumerable<Lib.Room>>(
          new List<Lib.Room>()
          {
            new Lib.Room()
          }
        ));
      var controller = new RoomsController(mockRepo.Object, mockServiceBus.Object);
      //act
      var result = await controller.GetFilteredRoomsAsync(Guid.NewGuid(), "", 1, "", "", DateTime.Now);

      //assert
      Assert.IsAssignableFrom<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteRoomAsyncShouldDeleteRoom()
    {
      var mockRepo = new Mock<IRepository>();
      var mockServiceBus = new Mock<IServiceBusSender>();

      Guid testGuid = Guid.NewGuid();


      var controller = new RoomsController(mockRepo.Object, mockServiceBus.Object);
      var result = await controller.DeleteRoomAsync(testGuid);

      Assert.IsAssignableFrom<NoContentResult>(result);
    }
  }
}
