using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Revature.Complex.Api.Models;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Revature.Complex.Api.Services
{
  public class RoomServiceSender : IRoomServiceSender
  {
    private readonly QueueClient _queueClient;
    private readonly ILogger<AddressServiceSender> _logger;

    /// <summary>
    /// ServiceBusSender constructor injected with IConfiguration and ILogger
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="logger"></param>
    public RoomServiceSender(IConfiguration configuration, ILogger<AddressServiceSender> logger)
    {
      _logger = logger;
      _queueClient = new QueueClient(configuration.GetConnectionString("ServiceBus"), configuration["Queues:RQueue"]);
    }

    /// <summary>
    /// ServiceBus message for sending a message about ApiRoomtoSend model
    /// </summary>
    /// <param name="roomToSend"></param>
    /// <returns></returns>
    public async Task SendRoomsMessages(IEnumerable<ApiRoomtoSend> rooms)
    {
      string data = JsonConvert.SerializeObject(rooms);

      Message message = new Message(Encoding.UTF8.GetBytes(data));

      _logger.LogInformation("ServiceBus sending creating message: ", data);
      await _queueClient.SendAsync(message);
    }
  }
}
