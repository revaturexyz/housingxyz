using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Revature.Tenant.Lib.Models;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusMessaging
{
  public class ServiceBusSender : IServiceBusSender
  {
    private readonly QueueClient _queueClient;
    private readonly IConfiguration _configuration;
    private const string QUEUE_NAME = "testing";
    private readonly ILogger<ServiceBusSender> _logger;

    public ServiceBusSender(IConfiguration configuration, ILogger<ServiceBusSender> logger)
    {
      _configuration = configuration;
      _logger = logger;
      _queueClient = new QueueClient(
        _configuration.GetConnectionString("Endpoint=sb"),
        QUEUE_NAME);
    }

    public async Task SendMessage(Tenant tenantToSend)
    {
      string data = JsonConvert.SerializeObject(tenantToSend);
      Message message = new Message(Encoding.UTF8.GetBytes(data));

      _logger.LogInformation("Service Bus sending message", data);
      await _queueClient.SendAsync(message);
    }
  }
}
