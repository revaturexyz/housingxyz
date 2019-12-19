using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Revature.Tenant.Api.Controllers;
using Revature.Tenant.Api.ServiceBus;
using Revature.Tenant.Lib.Interface;
using Xunit;

namespace Revature.Tenant.Tests.ApiTests.ControllerTests
{
  /// <summary>
  /// Unit tests for TenantRoomController Methods.
  /// </summary>
  public class TenantRoomControllerTests
  {
    /// <summary>
    /// GetTenantsNotAssignedARoom Should Return a List of Tenants not yet assigned to a Room.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task GetTenantsNotAssignedARoomShouldReturnAListofTenants()
    {
      //Arrange (Create mock DB and Test Data.)
      var mockRepo = new Mock<ITenantRoomRepository>();
      var mockRepo2 = new Mock<ITenantRepository>();
      var mockLogger = new Mock<ILogger<TenantRoomController>>();
      var mockRoomService = new Mock<IRoomService>();
      var mockServiceBusSender = new Mock<IServiceBusSender>();
      var _controller = new TenantRoomController(mockRepo.Object, mockRepo2.Object, mockLogger.Object, mockRoomService.Object, mockServiceBusSender.Object);

      //Act
      var result = await _controller.GetTenantsNotAssignedRoom();

      //Assert
      Assert.IsAssignableFrom<OkObjectResult>(result);
    }

    [Fact]
    public async Task AssignTenantShouldUpdateRoomIdAsync()
    {
      //Arrange (Create mock DB and Test Data.)
      var mockRepo = new Mock<ITenantRoomRepository>();
      var mockRepo2 = new Mock<ITenantRepository>();
      var mockLogger = new Mock<ILogger<TenantRoomController>>();
      var mockRoomService = new Mock<IRoomService>();
      var mockServiceBusSender = new Mock<IServiceBusSender>();
      var _controller = new TenantRoomController(mockRepo.Object, mockRepo2.Object, mockLogger.Object, mockRoomService.Object, mockServiceBusSender.Object);

      var roomId = Guid.NewGuid();
      var tenantId = Guid.NewGuid();

      mockRepo2.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Lib.Models.Tenant() { FirstName = "Marielle", Id = tenantId, Gender = "female" });

      var result = await _controller.AssignTenantToRoom(tenantId, roomId);

      Assert.IsAssignableFrom<NoContentResult>(result);
    }

    [Fact]
    public async Task AssignTenantShouldRejectUnknownTenantsAsync()
    {
      //Arrange (Create mock DB and Test Data.)
      var mockRepo = new Mock<ITenantRoomRepository>();
      var mockRepo2 = new Mock<ITenantRepository>();
      var mockLogger = new Mock<ILogger<TenantRoomController>>();
      var mockRoomService = new Mock<IRoomService>();
      var mockServiceBusSender = new Mock<IServiceBusSender>();
      var _controller = new TenantRoomController(mockRepo.Object, mockRepo2.Object, mockLogger.Object, mockRoomService.Object, mockServiceBusSender.Object);

      mockRepo2.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ThrowsAsync(new ArgumentNullException());

      var result = await _controller.AssignTenantToRoom(Guid.NewGuid(), Guid.NewGuid());

      Assert.IsAssignableFrom<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetTenantsByRoomIdShouldSucceedAsync()
    {
      //Arrange (Create mock DB and Test Data.)
      var mockRepo = new Mock<ITenantRoomRepository>();
      var mockRepo2 = new Mock<ITenantRepository>();
      var mockLogger = new Mock<ILogger<TenantRoomController>>();
      var mockRoomService = new Mock<IRoomService>();
      var mockServiceBusSender = new Mock<IServiceBusSender>();
      var _controller = new TenantRoomController(mockRepo.Object, mockRepo2.Object, mockLogger.Object, mockRoomService.Object, mockServiceBusSender.Object);

      mockRoomService.Setup(r => r.GetVacantRoomsAsync(It.IsAny<string>(), It.IsAny<DateTime>())).ReturnsAsync(new List<Lib.Models.AvailRoom>() { new Lib.Models.AvailRoom() { item1 = Guid.NewGuid(), item2 = 2 } });

      var result = await _controller.GetTenantsByRoomId("", DateTime.Now);

      Assert.IsAssignableFrom<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetTenantsShouldReturnBadRequestAsync()
    {
      //Arrange (Create mock DB and Test Data.)
      var mockRepo = new Mock<ITenantRoomRepository>();
      var mockRepo2 = new Mock<ITenantRepository>();
      var mockLogger = new Mock<ILogger<TenantRoomController>>();
      var mockRoomService = new Mock<IRoomService>();
      var mockServiceBusSender = new Mock<IServiceBusSender>();
      var _controller = new TenantRoomController(mockRepo.Object, mockRepo2.Object, mockLogger.Object, mockRoomService.Object, mockServiceBusSender.Object);

      mockRoomService.Setup(r => r.GetVacantRoomsAsync(It.IsAny<string>(), It.IsAny<DateTime>())).ThrowsAsync(new HttpRequestException());

      var result = await _controller.GetTenantsByRoomId("", DateTime.Now);

      Assert.IsAssignableFrom<BadRequestResult>(result);
    }
  }
}
