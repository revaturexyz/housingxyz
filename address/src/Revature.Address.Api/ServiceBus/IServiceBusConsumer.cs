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
