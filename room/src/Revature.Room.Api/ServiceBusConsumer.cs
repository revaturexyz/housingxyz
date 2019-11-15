using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Revature.Room.Lib;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System;
using Revature.Room.DataAccess;
using Serilog.Core;

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

    private readonly ILogger<Repository> _logger;

    public ServiceBusConsumer(IConfiguration configuration, IRepository repo, ILogger<Repository> logger)
    {
      _configuration = configuration;
      _repo = repo ?? throw new ArgumentNullException(); //TODO
      _queueClient = new QueueClient(
      _configuration.GetConnectionString("ServiceBus"), QUEUE_NAME);

      _logger = logger;
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
      // TODO: look
      try
      {
        _logger.LogInformation("Attempting to deserialize message from service bus consumer", message.Body);
        Room myRoom = JsonConvert.DeserializeObject<Room>(Encoding.UTF8.GetString(message.Body));

        // Persist our new data into the repository but not if Deserialization throws exception
        //Have to implement the CreateRoom in Repo, Nick told us not to use IEnumerables if possible

        await _repo.CreateRoom(myRoom);
       
      }
      catch (Exception ex)
      {
        // TODO: log exception properly
        //Console.WriteLine(ex.Message);
        //Declared a logger above and logged error here
        _logger.LogError(string.Format("Message did not convert properly.", message.Body), ex);
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
