using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revature.Tenant.Api.ServiceBus
{
  /// <summary>
  /// Interfance for ServiceBusConsumer
  /// </summary>
  public interface IServiceBusConsumer
  {
    void RegisterOnMessageHandlerAndReceiveMessages();
    Task CloseQueueAsync();
  }
}
