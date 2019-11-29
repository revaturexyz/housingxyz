using System;
using Xunit;
using System.Threading.Tasks;
using Revature.Tenant.Api.Controllers;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Revature.Tenant.Lib.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

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
      var mockConfiguration = new Mock<IConfiguration>();
      var mockClient = new Mock<IHttpClientFactory>();
      var _controller = new TenantRoomController(mockRepo.Object, mockRepo2.Object, mockLogger.Object, mockClient.Object, mockConfiguration.Object);

      //Act
      var result = await _controller.GetTenantsNotAssignedRoom();

      //Assert
      Assert.IsAssignableFrom<OkObjectResult>(result);
    }
  }
}
