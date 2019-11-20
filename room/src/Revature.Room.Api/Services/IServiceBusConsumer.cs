using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revature.Room.Api.Services
{
  /// <summary>
  /// Interface for ServiceBusConsumer
  /// </summary>
  public interface IServiceBusConsumer
  {
    void RegisterOnMessageHandlerAndReceiveMessages();

    Task CloseQueueAsync();
  }
}
