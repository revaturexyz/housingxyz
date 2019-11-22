using System.Threading.Tasks;
using Xunit;

using Moq;
using ServiceBusMessaging;
using System.Collections.Generic;
using System;
using System.Text;
using Microsoft.Azure.ServiceBus;
using System.Text.Json;
using Revature.Room.Lib.Models;

namespace Revature.Room.Tests.Revature.Room.Api.Tests
{
  public class ServiceBusTests
  {
    private readonly Mock<IQueueClient> _queClient;

    private readonly Mock<IServiceProvider> _serviceProvider;


    /// <summary>
    /// A WORK IN PROGRESS!
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task RecieveShouldReceiveMessage()
    {
      //Whatever you are testing do not mock it because it will undo all of what you are trying to test
      //Instead mock the dependencies that the class you are testing relies on

      ComplexMessage roomToSend = new ComplexMessage()
      {
        Room = new Lib.Room(),
        OperationType = new OperationType()
      };

      string data = JsonSerializer.Serialize(roomToSend);

      Message messageToSend = new Message(Encoding.UTF8.GetBytes(data));

      messageToSend.CorrelationId = "A1";

      //Assert

      //_queClient.Setup(m => m.);



    }
  }
}
