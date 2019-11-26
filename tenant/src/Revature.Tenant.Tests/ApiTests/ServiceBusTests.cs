using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Revature.Tenant.Api.ServiceBus;
using Xunit;

namespace Revature.Tenant.Tests.ApiTests
{
  public class ServiceBusTests
  {
    [Fact]
    public async Task SendRoomIdShouldSendRoomId()
    {
      //Arrange 
      Mock<IQueueClient> mqMock = new Mock<IQueueClient>();
      Mock<IConfiguration> configuration = new Mock<IConfiguration>();
      Mock<ILogger<ServiceBusSender>> logger = new Mock<ILogger<ServiceBusSender>>();

      var serviceBusSender = new ServiceBusSender(configuration.Object, logger.Object);

      //Act
      var result = serviceBusSender.SendRoomIdMessage(Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d"));

      //Assert
      var ok = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
      var tenant = Assert.IsAssignableFrom<ApiTenant>(ok.Value);

    }
  }
}
