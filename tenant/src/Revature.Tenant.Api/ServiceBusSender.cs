using Microsoft.Extensions.Configuration;
using Microsoft.ServiceBus.Messaging;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revature.Tenant.Api
{
  public class ServiceBusSender
  {
    //    private readonly QueueClient _queueClient;
    //    private readonly IConfiguration _configuration;
    //    private const string QUEUE_NAME = "simplequeue";

    //    public ServiceBusSender(IConfiguration configuration)
    //    {
    //      _configuration = configuration;
    //      _queueClient = new QueueClient(
    //        _configuration.GetConnectionString("ServiceBusConnectionString"),
    //        QUEUE_NAME);
    //    }

    //    public async Task SendMessage(Lib.Models.Tenant tenant)
    //    {
    //      string data = JsonConvert.SerializeObject(tenant);
    //      Message message = new Message(Encoding.UTF8.GetBytes(data));

    //      await _queueClient.SendAsync(message);
    //    }
  }
}
