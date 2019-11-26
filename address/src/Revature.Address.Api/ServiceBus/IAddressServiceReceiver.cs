using System.Threading.Tasks;

namespace Revature.Address.Api.ServiceBus
{
  /// <summary>
  /// Interface for ServiceBusConsumer
  /// </summary>
  interface IAddressServiceReceiver
  {
    abstract public void RegisterOnMessageHandlerAndReceiveMessages();
    abstract public Task CloseQueueAsync();
  }
}
