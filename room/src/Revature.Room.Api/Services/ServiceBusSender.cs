using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceBusMessaging
{
  /// <summary>
  /// This class' sole job is to serialize and send a mesesage to the queue to be verified
  /// </summary>
  public class ServiceBusSender : IServiceBusSender
  {
    private readonly QueueClient _deleteReceiptQueue;
    private readonly ILogger<ServiceBusSender> _logger;

    /// <summary>
    /// ServiceBusSender constructor injected with IConfiguration and ILogger
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="logger"></param>
    public ServiceBusSender(IConfiguration configuration, ILogger<ServiceBusSender> logger)
    {
      _logger = logger;
      //_deleteReceiptQueue = new QueueClient(configuration.GetConnectionString("ServiceBus"), configuration["Queues:DQueue"]);
      _deleteReceiptQueue = new QueueClient(configuration.GetConnectionString("ServiceBus"), configuration["Queues:SendQueue"]);
    }

    /// <summary>
    /// ServiceBus message for deleting all rooms in a complex
    /// </summary>
    /// <param name="roomToSend"></param>
    /// <returns></returns>
    public async Task SendDeleteMessage(Guid roomToSend)
    {
      string data = JsonSerializer.Serialize(roomToSend);

      Message message = new Message(Encoding.UTF8.GetBytes(data));

      _logger.LogInformation("ServiceBus sending delete message: ", data);
      await _deleteReceiptQueue.SendAsync(message);
      _logger.LogInformation("Success! ServiceBus sent delete message: ", data);
    }
  }
}
