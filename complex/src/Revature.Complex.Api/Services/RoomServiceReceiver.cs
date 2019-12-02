using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Revature.Complex.Api.Services;
using Revature.Complex.Lib;
using Revature.Complex.Lib.Interface;
using Revature.Complex.Lib.Models;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Revature.Complex.Api.Services
{
    /// <summary>
    /// This classes purpose is to connect to the queue and listen/receive a message sent from the complex and tenant service.
    /// Based on their message we will call upon the repository accordingly
    /// </summary>
    public class RoomServiceReceiver : BackgroundService, IRoomServiceReceiver
    {
      private readonly QueueClient _queueClient;
      private readonly IServiceProvider Services;
      private readonly ILogger<RoomServiceReceiver> log;

      /// <summary>
      /// Constructor injecting IConfiguration, IServiceProvider, and ILogger
      /// </summary>
      /// <param name="configuration"></param>
      /// <param name="services"></param>
      /// <param name="logger"></param>
      public RoomServiceReceiver(IConfiguration configuration, IServiceProvider services, ILogger<RoomServiceReceiver> logger)
      {
        _queueClient = new QueueClient(configuration.GetConnectionString("ServiceBus"), configuration["Queues:DeletedRooms"]);
        Services = services;
        log = logger;
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
      /// Receives and deserializes the message from room service.
      /// It should be 
      /// </summary>
      /// <param name="message"></param>
      /// <param name="token"></param>
      /// <returns></returns>
      private async Task ProcessMessagesAsync(Message message, CancellationToken token)
      {
        // Dispose of this scope after done using repository service
        // Necessary due to singleton service (bus service) consuming a scoped service (repo)
        using (var scope = Services.CreateScope())
        {
          var _repo = scope.ServiceProvider.GetRequiredService<IRepository>();
          try
          {
            log.LogInformation("Attempting to deserialize message from service bus consumer", message.Body);
            Guid listOfRoomId = JsonSerializer.Deserialize<Guid>(message.Body);

          log.LogInformation("Attempting to DELETE rooms based on COMPLEX ID", listOfRoomId);
          //foreach (Guid roomId in listOfRoomId)
          //{
          //  await _repo.DeleteAmenityRoomAsync(roomId);
          //  log.LogInformation("Amenity of Room Id: {RoomId} is deleted", roomId);
          //}
          await _repo.DeleteAmenityRoomAsync(listOfRoomId);
          log.LogInformation("Amenity of Room Id: {RoomId} is deleted", listOfRoomId);
        }
          catch (Exception ex)
          {
            log.LogError("Message did not convert properly", ex);
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
        log.LogError(context.ToString());
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
      RegisterOnMessageHandlerAndReceiveMessages();
      return Task.CompletedTask;
    }
  }
}
