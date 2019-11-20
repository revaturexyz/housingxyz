using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Revature.Address.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revature.Address.Api.ServiceBus
{
  public class ServiceBusSender : IServiceBusSender
  {
    private readonly QueueClient _queueClient;
    private readonly IConfiguration _configuration;
    private const string QUEUE_NAME = "simplequeue";

    public ServiceBusSender(IConfiguration configuration)
    {
      _configuration = configuration;
      _queueClient = new QueueClient(
        _configuration.GetConnectionString("ServiceBus"),
        QUEUE_NAME);
    }

    public async Task SendMessage(Lib.Address sentAddress)
    {
      string data = JsonConvert.SerializeObject(sentAddress);
      Message message = new Message(Encoding.UTF8.GetBytes(data));

      await _queueClient.SendAsync(message);
    }
  }
}
