using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using System;
using Revature.Room.Lib;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Web;
using System.Globalization;

namespace ServiceBusMessaging
{
  public class ServiceBusSender
  {
    //The connection string can be found from the Azure portal
    //in the shared access policies
    private const string QUEUE_NAME = "TestQ";
    private readonly IConfiguration _configuration;
    private readonly QueueClient _queueClient;
    private readonly ILogger<ServiceBusSender> _logger;

    public ServiceBusSender(IConfiguration configuration, ILogger<ServiceBusSender> logger)
    {
      _configuration = configuration;
      _logger = logger;
      _queueClient = new QueueClient(_configuration.GetConnectionString("ServiceBus"), QUEUE_NAME);

    }

    //Method to generate and get SAS token for service bus
    public static string GetSasToken(string resourceUri, string keyName, string key, TimeSpan ttl)
    {
      var expiry = GetExpiry(ttl);
      string stringToSign = HttpUtility.UrlEncode(resourceUri) + "\n" + expiry;
      HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
      var signature = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(stringToSign)));
      var sasToken = String.Format(CultureInfo.InvariantCulture, "SharedAccessSignature sr={0}&sig={1}&se={2}&skn={3}",
      HttpUtility.UrlEncode(resourceUri), HttpUtility.UrlEncode(signature), expiry, keyName);
      return sasToken;
    }

    //Get the time limit or expiration date of the SAS token
    private static string GetExpiry(TimeSpan ttl)
    {
      TimeSpan expirySinceEpoch = DateTime.UtcNow - new DateTime(1970, 1, 1) + ttl;
      return Convert.ToString((int)expirySinceEpoch.TotalSeconds);
    }
    string queueUrl = "https://gabservice.servicebus.windows.net/" + "queue" + "/messages";
    //string token = GetSasToken(queueUrl, "Key", "value", TimeSpan.FromDays(1));


    //Have to figure out how to differentiate between the HTTP CRUD requests when sending a message
    //Service bus doesn't have to be RESTFUL, we only need to care about sending message via Service bus
    //to POST, PUT, and DELETE.

    //LEARNED THE ROOM SERVICE IS NOT SENDING ANYTHING, JUST RECEIVING.
    //WILL REMOVE UNNECESSARY SEND MESSAGES

    //ServiceBus message for creating a room
    public async Task SendCreateMessage(Room roomToSend)
    {
      string data = JsonConvert.SerializeObject(roomToSend);

      Message message = new Message(Encoding.UTF8.GetBytes(data));

      _logger.LogInformation("ServiceBus sending create message: ", data);
      await _queueClient.SendAsync(message);
    }

    //ServiceBus message for updating a room
    public async Task SendUpdateMessage(Room roomToSend)
    {
      string data = JsonConvert.SerializeObject(roomToSend);

      Message message = new Message(Encoding.UTF8.GetBytes(data));

      _logger.LogInformation("ServiceBus sending update message: ", data);
      await _queueClient.SendAsync(message);
    }

    //ServiceBus message for deleting a room
    public async Task SendDeleteMessage(Room roomToSend)
    {
      string data = JsonConvert.SerializeObject(roomToSend);

      Message message = new Message(Encoding.UTF8.GetBytes(data));

      _logger.LogInformation("ServiceBus sending delete message: ", data);
      await _queueClient.SendAsync(message);
    }


  }
}
