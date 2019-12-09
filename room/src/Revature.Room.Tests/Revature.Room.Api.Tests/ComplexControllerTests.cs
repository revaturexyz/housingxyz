using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Revature.Room.Api.Controllers;
using Revature.Room.Lib;
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
      var mockLogger = new Mock<ILogger<ComplexController>>();

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
      var controller = new ComplexController(mockRepo.Object, mockLogger.Object);

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
      var mockLogger = new Mock<ILogger<ComplexController>>();

      mockRepo.Setup(r => r.GetFilteredRoomsAsync(
        It.IsAny<Guid>(),
        It.IsAny<string>(),
        It.IsAny<int>(),
        It.IsAny<string>(),
        It.IsAny<string>(),
        It.IsAny<DateTime>(),
        It.IsAny<Guid>()))
        .Throws(new KeyNotFoundException());

      var controller = new ComplexController(mockRepo.Object, mockLogger.Object);

      var result = await controller.GetFilteredRoomsAsync(Guid.NewGuid(), "", 1, "", "", DateTime.Now, Guid.NewGuid());

      Assert.IsType<NotFoundObjectResult>(result);
    }

    /// <summary>
    /// Unit test for checking if room creation was successful method returns appropriate response
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task PostRoomShouldCreateRoom()
    {
      //arrange
      var mockRepo = new Mock<IRepository>();
      var mockLogger = new Mock<ILogger<ComplexController>>();

      mockRepo.Setup(r => r.CreateRoomAsync(
        It.IsAny<Lib.Room>()
        ));

      var controller = new ComplexController(mockRepo.Object, mockLogger.Object);

      //act
      var roomTest = new Revature.Room.Lib.Room
      {
        ComplexId = Guid.NewGuid(),
        RoomId = Guid.NewGuid(),
        RoomNumber = "ABC",
        NumberOfBeds = 4,
        NumberOfOccupants = 4,
        Gender = "Male",
        RoomType = "Apartment"
      };

      roomTest.SetLease(DateTime.Now, DateTime.Today.AddDays(3));
      var result = await controller.PostRoomAsync(roomTest);

      //assert
      Assert.IsAssignableFrom<CreatedAtRouteResult>(result);
    }

    /// <summary>
    /// Checks if the room creation was unsuccessful, returns bad request
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task PostRoomShouldReturnBadRequest()
    {
      //arrange
      var mockRepo = new Mock<IRepository>();
      var mockLogger = new Mock<ILogger<ComplexController>>();

      mockRepo.Setup(r => r.CreateRoomAsync(
        It.IsAny<Lib.Room>()
        )).Throws(new ArgumentException());

      var controller = new ComplexController(mockRepo.Object, mockLogger.Object);

      //act
      var roomTest = new Lib.Room();

      var result = await controller.PostRoomAsync(roomTest);

      //assert
      Assert.IsType<BadRequestResult>(result);
    }

    /// <summary>
    /// Checks if room update was successful, method returns appropriate response
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task PutRoomShouldUpdateRoom()
    {
      //arrange
      var mockRepo = new Mock<IRepository>();
      var mockLogger = new Mock<ILogger<ComplexController>>();

      mockRepo.Setup(r => r.UpdateRoomAsync(
        It.IsAny<Lib.Room>()
        ));
      mockRepo.Setup(r => r.ReadRoomAsync(It.IsAny<Guid>())).Returns(Task.FromResult<Lib.Room>(new Lib.Room()));

      var controller = new ComplexController(mockRepo.Object, mockLogger.Object);

      //act
      var roomTest = new Revature.Room.Lib.Room
      {
        ComplexId = Guid.NewGuid(),
        RoomId = Guid.NewGuid(),
        RoomNumber = "ABC",
        NumberOfBeds = 4,
        NumberOfOccupants = 4,
        Gender = "Male",
        RoomType = "Apartment"
      };

      roomTest.SetLease(DateTime.Now, DateTime.Today.AddDays(3));

      var result = await controller.PutRoomAsync(Guid.NewGuid(), roomTest);
      //assert
      Assert.IsAssignableFrom<NoContentResult>(result);
    }

    /// <summary>
    /// Checks if updating was not successful, when the room isn't found, returns notfound
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task PutRoomShouldReturnNotFound()
    {
      //arrange
      var mockRepo = new Mock<IRepository>();

      var mockLogger = new Mock<ILogger<ComplexController>>();

      mockRepo.Setup(r => r.UpdateRoomAsync(
        It.IsAny<Lib.Room>()
        )).Throws(new InvalidOperationException());

      mockRepo.Setup(r => r.ReadRoomAsync(It.IsAny<Guid>())).Returns(Task.FromResult<Lib.Room>(new Lib.Room()));

      var controller = new ComplexController(mockRepo.Object, mockLogger.Object); ;

      //act
      var roomTest = new Lib.Room();
      //Need to set lease or else we will get an Argument Exception instead of InvalidOperation Exception
      roomTest.SetLease(DateTime.Now, DateTime.Now.AddDays(3));

      var result = await controller.PutRoomAsync(Guid.NewGuid(), roomTest);

      //assert
      Assert.IsType<NotFoundResult>(result);
    }

    /// <summary>
    /// Checks if update was unsuccessful due to bad user input, returns bad request
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task PutRoomShouldReturnBadRequest()
    {
      //arrange
      var mockRepo = new Mock<IRepository>();

      var mockLogger = new Mock<ILogger<ComplexController>>();

      mockRepo.Setup(r => r.UpdateRoomAsync(
        It.IsAny<Lib.Room>()
        )).Throws(new ArgumentException());

      mockRepo.Setup(r => r.ReadRoomAsync(It.IsAny<Guid>())).Returns(Task.FromResult<Lib.Room>(new Lib.Room()));

      var controller = new ComplexController(mockRepo.Object, mockLogger.Object); ;

      //act
      var roomTest = new Lib.Room();

      var result = await controller.PutRoomAsync(Guid.NewGuid(), roomTest);

      //assert
      Assert.IsType<BadRequestResult>(result);
    }

    /// <summary>
    /// Checks if get room was successful, returns appropriate response
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task GetRoomShouldReturnRoom()
    {
      //arrange
      var mockRepo = new Mock<IRepository>();
      var mockLogger = new Mock<ILogger<ComplexController>>();

      mockRepo.Setup(r => r.ReadRoomAsync(
        It.IsAny<Guid>())).Returns(Task.FromResult<Lib.Room>(
          new Lib.Room()
          ));
      var controller = new ComplexController(mockRepo.Object, mockLogger.Object);

      //act
      var result = await controller.GetRoomAsync(Guid.NewGuid());
      //assert
      Assert.IsAssignableFrom<OkObjectResult>(result);
    }

    /// <summary>
    /// Checks if the room was not found, method returns not found
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task GetRoomShouldReturnNotFound()
    {
      //arrange
      var mockRepo = new Mock<IRepository>();
      var mockLogger = new Mock<ILogger<ComplexController>>();

      mockRepo.Setup(r => r.ReadRoomAsync(
        It.IsAny<Guid>())).Throws(new InvalidOperationException());

      var controller = new ComplexController(mockRepo.Object, mockLogger.Object);
      //act
      var result = await controller.GetRoomAsync(Guid.NewGuid());
      //assert
      Assert.IsType<NotFoundResult>(result);
    }

    /// <summary>
    /// Checks if room was actually deleted, returns appropriate response
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task DeleteRoomShouldDelete()
    {
      var mockRepo = new Mock<IRepository>();
      var mockLogger = new Mock<ILogger<ComplexController>>();

      mockRepo.Setup(r => r.DeleteRoomAsync(
        It.IsAny<Guid>()));

      var controller = new ComplexController(mockRepo.Object, mockLogger.Object);

      //act
      var result = await controller.DeleteRoomAsync(Guid.NewGuid());

      //assert
      Assert.IsAssignableFrom<NoContentResult>(result);
    }

    /// <summary>
    /// Checks if deletion was unsuccessful due to room not in Db, returns not found
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task DeleteRoomShouldReturnGuidNotFound()
    {
      //arrange
      var mockRepo = new Mock<IRepository>();
      var mockLogger = new Mock<ILogger<ComplexController>>();

      mockRepo.Setup(r => r.DeleteRoomAsync(
        It.IsAny<Guid>())).Throws(new InvalidOperationException());

      var controller = new ComplexController(mockRepo.Object, mockLogger.Object);

      //act
      var result = await controller.DeleteRoomAsync(Guid.NewGuid());

      //assert
      Assert.IsType<NotFoundResult>(result);
    }

    /// <summary>
    /// Checks if when deleting rooms from a complex is successful, returns an appropriate response
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task DeleteComplexShouldDeleteComplexAndTheirAssociatingRooms()
    {
      //arrange
      var mockRepo = new Mock<IRepository>();
      var mockLogger = new Mock<ILogger<ComplexController>>();

      mockRepo.Setup(r => r.DeleteComplexRoomAsync(
        It.IsAny<Guid>()));

      var controller = new ComplexController(mockRepo.Object, mockLogger.Object);

      //act
      var result = await controller.DeleteComplexAsync(Guid.NewGuid());

      //assert
      Assert.IsAssignableFrom<NoContentResult>(result);
    }

    /// <summary>
    /// Checks if deleting rooms of a complex was unsucessful becaue room to be deleted was not found, returns not found
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task DeleteComplexShouldReturnNotFound()
    {
      //arrange
      var mockRepo = new Mock<IRepository>();
      var mockLogger = new Mock<ILogger<ComplexController>>();

      mockRepo.Setup(r => r.DeleteComplexRoomAsync(
        It.IsAny<Guid>())).Throws(new InvalidOperationException());

      var controller = new ComplexController(mockRepo.Object, mockLogger.Object);

      //act
      var result = await controller.DeleteComplexAsync(Guid.NewGuid());

      //assert
      Assert.IsType<NotFoundResult>(result);
    }
  }
}
