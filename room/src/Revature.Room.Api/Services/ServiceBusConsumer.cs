using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Revature.Room.Api.Services;
using Revature.Room.Lib;
using Revature.Room.Lib.Models;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusMessaging
{
  /// <summary>
  /// This classes purpose is to connect to the queue and listen/receive a message sent from the complex and tenant service.
  /// Based on their message we will call upon the repository accordingly
  /// </summary>
  public class ServiceBusConsumer : BackgroundService, IServiceBusConsumer
  {
    private readonly IConfiguration _configuration;
    private readonly QueueClient _queueClient;
    private readonly IServiceProvider Services;
    private readonly ILogger<ServiceBusConsumer> _logger;

    /// <summary>
    /// Constructor injecting IConfiguration, IServiceProvider, and ILogger
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="services"></param>
    /// <param name="logger"></param>
    public ServiceBusConsumer(IConfiguration configuration, IServiceProvider services, ILogger<ServiceBusConsumer> logger)
    {
      _configuration = configuration;
      _queueClient = new QueueClient(
      _configuration.GetConnectionString("ServiceBus"), _configuration.GetConnectionString("TestQ"));
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
      using (var scope = Services.CreateScope())
      {
        var _repo = scope.ServiceProvider.GetRequiredService<IRepository>();
        try
        {
          _logger.LogInformation("Attempting to deserialize message from service bus consumer", message.Body);
          ComplexMessage myRoom = JsonSerializer.Deserialize<ComplexMessage>(message.Body, new JsonSerializerOptions());

          // Persist our new data into the repository but not if Deserialization throws an exception
          //Operation type is the CUD that you want to implement like create, update, or delete
          //Case 0 = create, Case 1 = update, Case 2 = delete
          //We will listen for what the complex service will send us and determine
          //what CRUD operation to do based on the operationType
          switch (myRoom.operationType)
          {
            case 0:
              await _repo.CreateRoomAsync(myRoom.room);
              break;

            case 1:
              await _repo.UpdateRoomAsync(myRoom.room);
              break;

            case 2:
              await _repo.DeleteRoomAsync(myRoom.room.RoomId);
              break;

            default:

              break;
          }
        }
        catch (Exception ex)
        {
          _logger.LogError("Message did not convert properly", ex);
        }
        finally
        {
          // Alert bus service that message was received
          await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }
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
