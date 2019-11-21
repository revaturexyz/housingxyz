
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
    private readonly IServiceProvider Services;
    private readonly ILogger<ServiceBusConsumer> _logger;

    public ServiceBusConsumer(
        IConfiguration configuration,
        IServiceProvider services,
        ILogger<ServiceBusConsumer> logger)
    {
      _configuration = configuration;
      _logger = logger;
      Services = services;
      _queueClient = new QueueClient(
        _configuration.GetConnectionString("ServiceBus"), _configuration.GetConnectionString("TestQ"));
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
    /// Receives and deserializes the message from complex and tenant service.  Based on what they send
    /// us this method will determine what CRUD operations to do to the room service.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async Task ProcessMessagesAsync(Message message, CancellationToken token)
    {
      // Dispose of this scope after done using repository service
      //   Necessary due to singleton service (bus service) consuming a scoped service (repo)
      using var scope = Services.CreateScope();
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

    /// <summary>
    /// The exception handler for receiving a message.
    /// </summary>
    /// <param name="exceptionReceivedEventArgs"></param>
    /// <returns></returns>
    private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
    {
      var context = exceptionReceivedEventArgs.ExceptionReceivedContext;

      return Task.CompletedTask;
    }

    /// <summary>
    /// Closes the queue after receiving the message.
    /// </summary>
    /// <returns></returns>
    public async Task CloseQueueAsync()
    {
      await _queueClient.CloseAsync();
    }

    /// <summary>
    /// Inherited from the Background service, so far no use for it just yet
    /// </summary>
    /// <param name="stoppingToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException">Inherited but not utilized</exception>
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
      throw new NotImplementedException();
    }
  }
}

