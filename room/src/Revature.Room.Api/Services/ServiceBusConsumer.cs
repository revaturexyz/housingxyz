using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Revature.Room.Api.Services;
using Revature.Room.Lib;
using Revature.Room.Lib.Models;
using System;
using System.Collections.Generic;
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
    //_roomDUCQueue will be used for receiving from the Complex service
    private readonly QueueClient _roomDUCQueue;

    //_occupancyUpdateQueue will be used for receiving from the Tenant service
    private readonly QueueClient _occupancyUpdateQueue;

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
      //Changed this from testq to complexq
      //Might have to make another queclient for tenant

      //Changed connection and queues to test
      _roomDUCQueue = new QueueClient(configuration.GetConnectionString("MyServiceBus"), configuration["Queues:MyQueue"]);

      _occupancyUpdateQueue = new QueueClient(configuration.GetConnectionString("ServiceBus"), configuration["Queues:TQueue"]);
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
        MaxConcurrentCalls = 2,
        AutoComplete = false
      };
      
      _roomDUCQueue.RegisterMessageHandler(ProcessRoomDUCAsync, messageHandlerOptions);
      _occupancyUpdateQueue.RegisterMessageHandler(ProcessOccupancyUpdateAsync, messageHandlerOptions);
    }
    /// <summary>
    /// Method used to process messages from tenant service
    /// </summary>
    /// <param name="message"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    /// <exception cref="JsonException">Thrown when message isn't deserialized properly</exception>
    private async Task ProcessOccupancyUpdateAsync(Message message, CancellationToken token)
    {
      // Dispose of this scope after done using repository service
      //   Necessary due to singleton service (bus service) consuming a scoped service (repo)
      using (var scope = Services.CreateScope())
      {
        var _repo = scope.ServiceProvider.GetRequiredService<IRepository>();
        try
        {
          _logger.LogInformation("Attempting to deserialize tenant message from service bus consumer", message.Body);
          // Expect to receive a tuple of <Guid, string> from the tenant service
          TenantMessage receivedMessage = JsonSerializer.Deserialize<TenantMessage>(message.Body);

          //TenantMessage returns both a Tuple that has a type of <Guid, string> and an operation type that
          //states which operation to do, either adding or subtracting an occupant
          switch (receivedMessage.OperationType) {
            case OperationType.Create:
              _logger.LogInformation("Adding occupants to a room", message.Body);

              await _repo.AddRoomOccupantsAsync(receivedMessage.Tenant.Item1, receivedMessage.Tenant.Item2);

              break;
            case OperationType.Delete:
              _logger.LogInformation("Subtracting an occupant from a room", message.Body);

              await _repo.SubtractRoomOccupantsAsync(receivedMessage.Tenant.Item1);

              break;
          }

        }
        catch (JsonException ex)
        {
          _logger.LogError("Message did not convert properly", ex);
        }
        finally
        {
          // Alert bus service that message was received
          await _occupancyUpdateQueue.CompleteAsync(message.SystemProperties.LockToken);
        }
      }
    }

    /// <summary>
    /// The actual method to process the received message from complex service.
    /// Receives and deserializes the message from complex service.  Based on what they send
    /// us this method will determine what CRUD operations to do on the Room service.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async Task ProcessRoomDUCAsync(Message message, CancellationToken token)
    {
      // Dispose of this scope after done using repository service
      //   Necessary due to singleton service (bus service) consuming a scoped service (repo)
      using (var scope = Services.CreateScope())
      {
        var _repo = scope.ServiceProvider.GetRequiredService<IRepository>();
        try
        {
          _logger.LogInformation("Attempting to deserialize complex message from service bus consumer: {message}", message.Body);

          ComplexMessage receivedMessage = JsonSerializer.Deserialize<ComplexMessage>(message.Body);

            _logger.LogInformation("Showing RoomNumber: {message}", receivedMessage.RoomNumber);
            Room myRoom = new Room()
            {
              RoomId = receivedMessage.RoomId,
              RoomNumber = receivedMessage.RoomNumber,
              ComplexId = receivedMessage.ComplexId,
              NumberOfBeds = receivedMessage.NumberOfBeds,
              RoomType = receivedMessage.RoomType,
              NumberOfOccupants = 0
              
            };
             myRoom.SetLease(receivedMessage.LeaseStart, receivedMessage.LeaseEnd);

             _logger.LogInformation("Deserialized you hoe: {message}", myRoom.RoomNumber);

             _logger.LogInformation("Deserialized with this operator: {message}", receivedMessage.QueOperator);
            // Persist our new data into the repository but not if Deserialization throws an exception
            //Operation type is the CUD that you want to implement like create, update, or delete
            // Case 0 = create, Case 1 = update, Case 2 = delete
            // We will listen for what the complex service will send us and determine
            // what CRUD operation to do based on the OperationType
            switch ((OperationType)receivedMessage.QueOperator)
            {
              case OperationType.Create:
                await _repo.CreateRoomAsync(myRoom);
                _logger.LogInformation("Created a room!!!: {message}", myRoom.RoomNumber);
                break;

              case OperationType.Update:
                await _repo.UpdateRoomAsync(myRoom);
                _logger.LogInformation("Updated a room!!!: {message}", myRoom);
                break;

              case OperationType.Delete:
                await _repo.DeleteRoomAsync(myRoom.RoomId);
                _logger.LogInformation("Deleted a room!!!: {message}", myRoom);
                break;
              case OperationType.DeleteCom:
                await _repo.DeleteComplexRoomAsync(myRoom.ComplexId);
                _logger.LogInformation("Oh my, you deleted a complex you naughty boy!: {message}", myRoom);
                break;
            }
        }
        catch (Exception ex)
        {
          _logger.LogError("Message did not convert properly: {message}", ex);
        }
        finally
        {
          // Alert bus service that message was received
          //_logger.LogInformation("Message has been received and successful.  It's been processed you skank: {message}", message.Body);
          await _roomDUCQueue.CompleteAsync(message.SystemProperties.LockToken);
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
      //_logger.LogInformation("No messages in queue", context.ToString());
      return Task.CompletedTask;
    }

    /// <summary>
    /// Closes the queue after receiving the message.
    /// </summary>
    /// <returns></returns>
    public async Task CloseQueueAsync()
    {
      await _roomDUCQueue.CloseAsync();
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
