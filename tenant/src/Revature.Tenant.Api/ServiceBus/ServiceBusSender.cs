using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Revature.Tenant.Lib.Models;
using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace Revature.Tenant.Api.ServiceBus
{
  /// <summary>
  /// The purpose of this class is to serialize and send a mesesage to the queue to be verified
  /// </summary>
  public class ServiceBusSender : IServiceBusSender
  {
    private readonly QueueClient _queueClient;
    private readonly IConfiguration _configuration;
    private const string QUEUE_NAME = "testing";
    private readonly ILogger<ServiceBusSender> _logger;

    /// <summary>
    /// ServiceBusSender constructor injected with IConfiguration and ILogger
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="logger"></param>
    public ServiceBusSender(IConfiguration configuration, ILogger<ServiceBusSender> logger)
    {
      _configuration = configuration;
      _logger = logger;
      _queueClient = new QueueClient(
        _configuration.GetConnectionString("ServiceBus"),
        QUEUE_NAME);
    }

    /// <summary>
    /// ServiceBus message for sending a tenant room id
    /// </summary>
    /// <param name="roomId"></param>
    public async Task SendRoomIdMessage(Guid roomId)
    {
      string data = JsonSerializer.Serialize(roomId);
      Message message = new Message(Encoding.UTF8.GetBytes(data));

      _logger.LogInformation("Service Bus is sending Room Id", data);
      await _queueClient.SendAsync(message);
    }

    /// <summary>
    /// ServiceBus message for sending a tenant's address and address id
    /// </summary>
    /// <param name="addressId"></param>
    /// <param name="address"></param>
    public async Task SendAddressIdMessage(Models.ApiAddress address)
    {
      string data = JsonSerializer.Serialize(address);
      Message message = new Message(Encoding.UTF8.GetBytes(data));

      _logger.LogInformation("Service Bus is sending Address and Address Id", data);
      await _queueClient.SendAsync(message);
    }
  }
}
