
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Revature.Tenant.Lib;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System;
using Revature.Tenant.DataAccess;
using Serilog.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Revature.Tenant.DataAccess.Repository;
using Revature.Tenant.Lib.Interface;
using Revature.Tenant.Lib.Models;

namespace Revature.Tenant.Api.ServiceBus
{
  /// <summary>
  /// This classes purpose is to connect to the queue and listen/receive a message sent
  /// from the Notification, Room, and Address service.
  /// Based on their message we will call upon the repository accordingly
  /// </summary>
  public class ServiceBusConsumer : BackgroundService, IServiceBusConsumer
  {
    private readonly IConfiguration _configuration;
    private readonly QueueClient _queueClient;
    private const string QUEUE_NAME = "testing";
    private readonly IServiceProvider Services;
    private readonly ILogger<TenantRepository> _logger;

    /// <summary>
    /// Constructor injecting IConfiguration, IServiceProvider, and ILogger
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="services"></param>
    /// <param name="logger"></param>
    public ServiceBusConsumer(IConfiguration configuration, IServiceProvider services, ILogger<TenantRepository> logger)
    {
      _configuration = configuration;
      _queueClient = new QueueClient(
      _configuration.GetConnectionString("ServiceBus"), QUEUE_NAME);
      Services = services;
      _logger = logger;
    }

    /// <summary>
    /// Registers the message and then calls the process message
    /// </summary>
    public void RegisterOnMessageHandlerAndReceiveMessages()
    {
      var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
      {
        MaxConcurrentCalls = 1,
        AutoComplete = false
      };

      _queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
    }

    /// <summary>
    /// The actual method to process the received message.
    /// Receives and deserializes the message from notification, room, and  service.
    /// Based on what they send us this method will determine what CRUD operations to do to the room service.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async Task ProcessMessagesAsync(Message message, CancellationToken token)
    {
      // Dispose of this scope after done using repository service
      //   Necessary due to singleton service (bus service) consuming a scoped service (repo)
      using (var scope = Services.CreateScope())
      {
        var _repo = scope.ServiceProvider.GetRequiredService<ITenantRepository>();
        try
        {
          _logger.LogInformation("Attempting to deserialize message from service bus consumer", message.Body);
          Lib.Models.Tenant myTenant = JsonConvert.DeserializeObject<Lib.Models.Tenant>(Encoding.UTF8.GetString(message.Body));

          // Persist our new data into the repository but not if Deserialization throws exception
          //Have to implement the CreateRoom in Repo, Nick told us not to use IEnumerables if possible

          await _repo.AddAsync(myTenant);

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
