using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Revature.Room.Lib;
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
  public class ServiceBusSender: IServiceBusSender
  {
    
    private readonly QueueClient _queueClient;
    private readonly ILogger<ServiceBusSender> _logger;

    /// <summary>
    /// ServiceBusSender constructor injected with IConfiguration and ILogger
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="logger"></param>
    public ServiceBusSender(IConfiguration configuration, ILogger<ServiceBusSender> logger)
    {
      _logger = logger;
      _queueClient = new QueueClient(_configuration.GetConnectionString("ServiceBus"), _configuration["Queues:CQueues"]);
    }


    /// <summary>
    /// ServiceBus message for deleting a complex 
    /// </summary>
    /// <param name="roomToSend"></param>
    /// <returns></returns>
    public async Task SendDeleteComplexMessage(List<Guid> roomToSend)
    {
      string data = JsonSerializer.Serialize(roomToSend);

      Message message = new Message(Encoding.UTF8.GetBytes(data));

      _logger.LogInformation("ServiceBus sending delete guid message: ", data);
      await _queueClient.SendAsync(message);
    }

    /// <summary>
    /// ServiceBus message for deleting a message
    /// </summary>
    /// <param name="roomToSend"></param>
    /// <returns></returns>
    public async Task SendDeleteMessage(Room roomToSend)
    {
      string data = JsonSerializer.Serialize(roomToSend);

      Message message = new Message(Encoding.UTF8.GetBytes(data));

      _logger.LogInformation("ServiceBus sending delete message: ", data);
      await _queueClient.SendAsync(message);
    }
  }
}
