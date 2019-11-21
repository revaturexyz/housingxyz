using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revature.Address.Api.ServiceBus
{
  /// <summary>
  /// Interface for ServiceBusConsumer
  /// </summary>
  interface IServiceBusConsumer
  {
    void RegisterOnMessageHandlerAndReceiveMessages();
    Task CloseQueueAsync();
  }
}
