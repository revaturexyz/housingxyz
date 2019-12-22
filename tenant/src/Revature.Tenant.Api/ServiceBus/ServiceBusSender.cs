using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Revature.Tenant.Lib.Models;

namespace Revature.Tenant.Api.ServiceBus
{
  /// <summary>
  /// The purpose of this class is to serialize and send a mesesage to the queue to be verified
  /// </summary>
  public class ServiceBusSender : IServiceBusSender
  {
    private readonly QueueClient _queueClient;
    private readonly IConfiguration _queueConfiguration;
    private readonly ILogger<ServiceBusSender> _logger;

    /// <summary>
    /// ServiceBusSender constructor injected with IConfiguration and ILogger
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="logger"></param>
    public ServiceBusSender(IConfiguration queueConfiguration, ILogger<ServiceBusSender> logger)
    {
      _queueConfiguration = queueConfiguration;
      _logger = logger;
      _queueClient = new QueueClient(
        _queueConfiguration.GetConnectionString("ServiceBus"),
        _queueConfiguration.GetSection("Queues")["AssignedRoom"]);
    }

    /// <summary>
    /// ServiceBus message for sending a tenant room id
    /// </summary>
    /// <param name="roomMessage">The details room service needs to update their rooms</param>
    public async Task SendRoomIdMessage(RoomMessage roomMessage)
    {
      var data = JsonSerializer.Serialize(roomMessage);
      var message = new Message(Encoding.UTF8.GetBytes(data));

      _logger.LogInformation("Service Bus is sending message with room id, gender, and operation type: {data}", data);

      await _queueClient.SendAsync(message);

      _logger.LogInformation("Message sent!");
    }
  }
}
