using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Revature.Room.Lib;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System;

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
    private readonly IRepository _repo;
    private const string QUEUE_NAME = "TestQ";

    public ServiceBusConsumer(IConfiguration configuration, IRepository repo)
    {
      _configuration = configuration;
      _repo = repo ?? throw new ArgumentNullException(); //TODO
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
      // Parse message into Business Logic room model
      // TODO: look
      try
      {
        Room myRoom = JsonConvert.DeserializeObject<Room>(Encoding.UTF8.GetString(message.Body));

        // Persist our new data into the repository but not if Deserialization throws exception
        _repo.CreateRoom(myRoom);
      }
      catch (Exception ex)
      {
        // log exception
      }
      finally
      {
        // Alert bus service that message was received
        await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
      }
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
