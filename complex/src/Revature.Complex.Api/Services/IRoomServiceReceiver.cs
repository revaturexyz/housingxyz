using System.Threading.Tasks;

namespace Revature.Complex.Api.Services
{
  public interface IRoomServiceReceiver
  {
    /// <summary>
    /// Registers the message and then calls the process message
    /// </summary>
    abstract public void RegisterOnMessageHandlerAndReceiveMessages();

    /// <summary>
    /// Closes the queue after receiving the message.
    /// </summary>
    /// <returns></returns>
    abstract public Task CloseQueueAsync();
  }
}
