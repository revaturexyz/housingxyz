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
        const string ServiceBusConnectionString = "Endpoint=sb://gabservice.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=jcHIEa2cBfoO+KI+quH3BJJqNt3JIq58fnJBI2Tjkik=";
        const string QueueName = "TestQ";
        private readonly QueueClient _queueClient;

        public ServiceBusSender(IConfiguration configuration)
        {
            _queueClient = new QueueClient(ServiceBusConnectionString, QueueName);

        }

        public async Task SendMessage(Room roomToSend)
        {
            string data = JsonConvert.SerializeObject(roomToSend);
            Message message = new Message(Encoding.UTF8.GetBytes(data));

            await _queueClient.SendAsync(message);
        }
    }
}
