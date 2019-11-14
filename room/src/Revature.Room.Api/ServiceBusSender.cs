using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using System;
using Revature.Room.Api.Models;

namespace ServiceBusMessaging
{
  public class ServiceBusSender
  {
    //The connection string can be found from the Azure portal
    //in the shared access policies
    private const string QUEUE_NAME = "TestQ";
    private readonly IConfiguration _configuration;
    private readonly QueueClient _queueClient;

    public ServiceBusSender(IConfiguration configuration)
    {
      _configuration = configuration;

      _queueClient = new QueueClient(_configuration.GetConnectionString("ServiceBus"), QUEUE_NAME);

    }

    public async Task SendMessage(Room roomToSend)
    {
        string data = JsonConvert.SerializeObject(roomToSend);
        Message message = new Message(Encoding.UTF8.GetBytes(data));

        await _queueClient.SendAsync(message);
    }
  }
}
