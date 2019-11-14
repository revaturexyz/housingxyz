using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Revature.Room.Api.Models;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusMessaging
{
  public interface IServiceBusConsumer
  {
    void RegisterOnMessageHandlerAndReceiveMessages();
    Task CloseQueueAsync();
  }

  public class ServiceBusConsumer : IServiceBusConsumer
  {
    private readonly IConfiguration _configuration;
    private readonly QueueClient _queueClient;
    private const string QUEUE_NAME = "TestQ";

    public ServiceBusConsumer(IConfiguration configuration)
    {
      _configuration = configuration;
      _queueClient = new QueueClient(
      _configuration.GetConnectionString("ServiceBus"), QUEUE_NAME);
    }

    public void RegisterOnMessageHandlerAndReceiveMessages()
    {
      var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
      {
        MaxConcurrentCalls = 1,
        AutoComplete = false
      };

      _queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
    }

    private async Task ProcessMessagesAsync(Message message, CancellationToken token)
    {
      var myRoom = JsonConvert.DeserializeObject<Room>(Encoding.UTF8.GetString(message.Body));
      await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
    }

    private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
    {
      var context = exceptionReceivedEventArgs.ExceptionReceivedContext;

      return Task.CompletedTask;
    }

    public async Task CloseQueueAsync()
    {
      await _queueClient.CloseAsync();
    }
  }
}
