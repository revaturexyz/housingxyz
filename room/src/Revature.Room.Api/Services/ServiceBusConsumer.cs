using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Revature.Room.Api.Services;
using Revature.Room.Lib;
using Revature.Room.Lib.Models;

namespace ServiceBusMessaging
{
  /// <summary>
  /// This classes purpose is to connect to the queue and listen/receive a message sent from the complex and tenant service.
  /// Based on their message we will call upon the repository accordingly
  /// </summary>
  public class ServiceBusConsumer : BackgroundService, IServiceBusConsumer
  {
    //ServiceBusSender is created here so we can use it to send something back to the complex
    //the moment we receive it.  (Deleting a complex)
    private readonly IServiceBusSender _serviceSender;

    //_roomDUCQueue will be used for receiving from the Complex service
    private readonly QueueClient _roomDUCQueue;

    //_occupancyUpdateQueue will be used for receiving from the Tenant service
    private readonly QueueClient _occupancyUpdateQueue;

    private readonly IServiceProvider _services;
    private readonly ILogger<ServiceBusConsumer> _logger;

    /// <summary>
    /// Constructor injecting IConfiguration, IServiceProvider, and ILogger
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="services"></param>
    /// <param name="logger"></param>
    public ServiceBusConsumer(IConfiguration configuration, IServiceProvider services, ILogger<ServiceBusConsumer> logger,
      IServiceBusSender sender)
    {

      //Have two queues because we're listening to 2 different services (complex and tenant service)
      _roomDUCQueue = new QueueClient(configuration.GetConnectionString("ServiceBus"), configuration["Queues:CQueue"]);

      _occupancyUpdateQueue = new QueueClient(configuration.GetConnectionString("ServiceBus"), configuration["Queues:TQueue"]);

      _services = services;
      _logger = logger;
      _serviceSender = sender;
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
      using var scope = _services.CreateScope();
      var repo = scope.ServiceProvider.GetRequiredService<IRepository>();
      try
      {
        _logger.LogInformation("Attempting to deserialize tenant message from service bus consumer", message.Body);
        // Expect to receive a tuple of <Guid, string> from the tenant service
        var receivedMessage = JsonSerializer.Deserialize<TenantMessage>(message.Body);

        //TenantMessage returns both a Tuple that has a type of <Guid, string> and an operation type that
        //states which operation to do, either adding or subtracting an occupant
        switch (receivedMessage.OperationType)
        {
          case OperationType.Create:
            _logger.LogInformation("Adding occupants to a room", message.Body);

            await repo.AddRoomOccupantsAsync(receivedMessage.RoomId, receivedMessage.Gender);
            break;
          case OperationType.Delete:
            _logger.LogInformation("Subtracting an occupant from a room", message.Body);

            await repo.SubtractRoomOccupantsAsync(receivedMessage.RoomId);
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
      using var scope = _services.CreateScope();
      var repo = scope.ServiceProvider.GetRequiredService<IRepository>();
      try
      {
        _logger.LogInformation("Attempting to deserialize complex message from service bus consumer: {message}", message.Body);

        var receivedMessage = JsonSerializer.Deserialize<ComplexMessage>(message.Body);

        _logger.LogInformation("Showing RoomNumber: {message}", receivedMessage.RoomNumber);

        //Did not put deleting complex in switch statement because of the constraints we ran into,
        //so we put it in an if-else statement instead
        if (receivedMessage.QueOperator == 3)
        {
          _logger.LogInformation("Deserialized with this operator: {message}", receivedMessage.QueOperator);

          _logger.LogInformation("Attempting to DELETE COMPLEX!!! {0}", receivedMessage.ComplexId);

          IEnumerable<Guid> listOfRooms = await repo.DeleteComplexRoomAsync(receivedMessage.ComplexId);

          _logger.LogInformation("Oh my, you DELETED a COMPLEX!: {message}", receivedMessage.ComplexId);

          _logger.LogInformation("Sending LIST of deleted room Ids!!! {0}", listOfRooms);

          foreach (var r in listOfRooms)
          {
            await _serviceSender.SendDeleteMessage(r);
          }

          _logger.LogInformation("Sent the list of rooms back to complex service!", listOfRooms);
        }
        //If not deleting complex then do the other operations with rooms
        else
        {
          var myRoom = new Room()
          {
            RoomId = receivedMessage.RoomId,
            RoomNumber = receivedMessage.RoomNumber,
            ComplexId = receivedMessage.ComplexId,
            NumberOfBeds = receivedMessage.NumberOfBeds,
            RoomType = receivedMessage.RoomType,
            NumberOfOccupants = 0,

          };

          myRoom.SetLease(receivedMessage.LeaseStart, receivedMessage.LeaseEnd);

          _logger.LogInformation("Deserialized your room!: {message}", myRoom.RoomNumber);


          _logger.LogInformation("Deserialized with this operator: {message}", receivedMessage.QueOperator);
          // Persist our new data into the repository but not if Deserialization throws an exception
          //Operation type is the CUD that you want to implement like create, update, or delete
          // Case 0 = create, Case 2 = update, Case 1 = delete
          // We will listen for what the complex service will send us and determine
          // what CRUD operation to do based on the OperationType
          switch ((OperationType)receivedMessage.QueOperator)
          {
            case OperationType.Create:
              _logger.LogInformation("Attempting to CREATE a room!!!", myRoom.RoomNumber);
              await repo.CreateRoomAsync(myRoom);
              _logger.LogInformation("Created a room!!!: {message}", myRoom.RoomNumber);
              break;

            case OperationType.Update:
              _logger.LogInformation("Attempting to UPDATE a room!!!", myRoom.RoomNumber);
              await repo.UpdateRoomAsync(myRoom);
              _logger.LogInformation("Updated a room!!!: {message}", myRoom);
              break;

            case OperationType.Delete:
              _logger.LogInformation("Attempting to DELETE a room!!!", myRoom.RoomNumber);
              await repo.DeleteRoomAsync(myRoom.RoomId);
              _logger.LogInformation("DELETED a room!!!: {message}", myRoom);
              break;
          }
        }
      }
      catch (Exception ex)
      {
        _logger.LogError("Message did not convert properly: {message}", ex);
      }
      finally
      {
        // Alert bus service that message was received
        await _roomDUCQueue.CompleteAsync(message.SystemProperties.LockToken);
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
      await _roomDUCQueue.CloseAsync();

      //Added this since the room service is listening from 2 services, thus we need 2 queues
      await _occupancyUpdateQueue.CloseAsync();
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
