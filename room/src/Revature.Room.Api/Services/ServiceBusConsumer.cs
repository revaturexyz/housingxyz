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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBusMessaging
{
  public interface IServiceBusConsumer
  {
    void RegisterOnMessageHandlerAndReceiveMessages();
    Task CloseQueueAsync();
  }

  public class ServiceBusConsumer : BackgroundService, IServiceBusConsumer 
  {
    private readonly IConfiguration _configuration;
    private readonly QueueClient _queueClient;
    private const string QUEUE_NAME = "TestQ";
    private readonly IServiceProvider Services;
    private readonly ILogger<Repository> _logger;

    public ServiceBusConsumer(IConfiguration configuration, IServiceProvider services, ILogger<Repository> logger)
    {
      _configuration = configuration;
      _queueClient = new QueueClient(
      _configuration.GetConnectionString("ServiceBus"), QUEUE_NAME);
      Services = services;
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
      // Dispose of this scope after done using repository service
      //   Necessary due to singleton service (bus service) consuming a scoped service (repo)
      using (var scope = Services.CreateScope())
      {
        var _repo = scope.ServiceProvider.GetRequiredService<IRepository>();
        try
        {
          _logger.LogInformation("Attempting to deserialize message from service bus consumer", message.Body);
          Room myRoom = JsonConvert.DeserializeObject<Room>(Encoding.UTF8.GetString(message.Body));

          // Persist our new data into the repository but not if Deserialization throws exception
          //Have to implement the CreateRoom in Repo, Nick told us not to use IEnumerables if possible

          await _repo.CreateRoomAsync(myRoom);

        }
        catch (Exception ex)
        {
          _logger.LogError(string.Format("Message did not convert properly.", message.Body), ex);
        }
        finally
        {
          // Alert bus service that message was received
          await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

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

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
      throw new NotImplementedException();
    }
  }
}
