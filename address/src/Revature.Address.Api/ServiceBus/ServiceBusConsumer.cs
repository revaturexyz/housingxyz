
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Revature.Address.Lib.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Revature.Address.Api.ServiceBus
{
  public class ServiceBusConsumer : BackgroundService, IServiceBusConsumer
  {
    private readonly IConfiguration _configuration;
    private readonly QueueClient _queueClient;
    private const string QUEUE_NAME = "simplequeue";
    private readonly IServiceProvider Services;
    private readonly ILogger<DataAccess.DataAccess> _logger;

    public ServiceBusConsumer(
        IConfiguration configuration,
        IServiceProvider services,
        ILogger<DataAccess.DataAccess> logger)
    {
      _configuration = configuration;
      _logger = logger;
      Services = services;
      _queueClient = new QueueClient(
        _configuration.GetConnectionString("ServiceBusConnectionString"), QUEUE_NAME);
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
        var _repo = scope.ServiceProvider.GetRequiredService<IDataAccess>();
        try
        {
          _logger.LogInformation("Attempting to deserialize message from service bus consumer", message.Body);
          Lib.Address myAddress = JsonConvert.DeserializeObject<Lib.Address>(Encoding.UTF8.GetString(message.Body));

          // Persist our new data into the repository but not if Deserialization throws exception
          //Have to implement the CreateRoom in Repo, Nick told us not to use IEnumerables if possible

          await _repo.AddAddressAsync(myAddress);

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
      _logger.LogError(exceptionReceivedEventArgs.Exception, "Message handler encountered an exception");
      var context = exceptionReceivedEventArgs.ExceptionReceivedContext;

      _logger.LogDebug($"- Endpoint: {context.Endpoint}");
      _logger.LogDebug($"- Entity Path: {context.EntityPath}");
      _logger.LogDebug($"- Executing Action: {context.Action}");

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

