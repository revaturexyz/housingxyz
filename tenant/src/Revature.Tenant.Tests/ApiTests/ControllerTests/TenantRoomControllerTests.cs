using System;
using Xunit;
using System.Threading.Tasks;
using Revature.Tenant.Api.Controllers;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Revature.Tenant.Lib.Interface;
using Microsoft.Extensions.Logging;

namespace Revature.Tenant.Tests.ApiTests.ControllerTests
{
  /// <summary>
  /// Unit tests for TenantRoomController Methods.
  /// </summary>
  public class TenantRoomControllerTests
  {
    /// <summary>
    /// GetTenantsByRoomId Should Return a List of Rooms with Tenants.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task GetTenantsByRoomIdShouldReturnList()
    {
      //Arrange (Create mock DB and Test Data.)
      var mockRepo = new Mock<ITenantRoomRepository>();
      var mockLogger = new Mock<ILogger<TenantRoomController>>();

      var _controller = new TenantRoomController(mockRepo.Object, mockLogger.Object);
      var _gender = "Female";
      var _endDate = new DateTime(2019, 12, 31);

      //Act
      var result = await _controller.GetTenantsByRoomId(_gender, _endDate);

      //Assert
      Assert.IsAssignableFrom<OkObjectResult>(result);
    }
    /// <summary>
    /// GetTenantsNotAssignedARoom Should Return a List of Tenants not yet assigned to a Room.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task GetTenantsNotAssignedARoomShouldReturnAListofTenants()
    {
      //Arrange (Create mock DB and Test Data.)
      var mockRepo = new Mock<ITenantRoomRepository>();
      var mockLogger = new Mock<ILogger<TenantRoomController>>();

      var _controller = new TenantRoomController(mockRepo.Object, mockLogger.Object);

      //Act
      var result = await _controller.GetTenantsNotAssignedRoom();

      //Assert
      Assert.IsAssignableFrom<OkObjectResult>(result);
    }
  }
}
