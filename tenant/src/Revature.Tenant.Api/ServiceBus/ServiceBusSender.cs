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
    /// Method to generate and get Shared Access Signature which helps verify that you are authorized to interact with the queue
    /// </summary>
    /// <param name="resourceUri"></param>
    /// <param name="keyName"></param>
    /// <param name="key"></param>
    /// <param name="ttl"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Get the time limit or expiration of the SAS token
    /// </summary>
    /// <param name="ttl"></param>
    /// <returns></returns>
    private static string GetExpiry(TimeSpan ttl)
    {
      TimeSpan expirySinceEpoch = DateTime.UtcNow - new DateTime(1970, 1, 1) + ttl;
      return Convert.ToString((int)expirySinceEpoch.TotalSeconds);
    }

    /// <summary>
    /// ServiceBus message for sending a tenant room id
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
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
    /// <returns></returns>
    public async Task SendAddressIdMessage(string address)
    {
      string data = JsonSerializer.Serialize(address);
      Message message = new Message(Encoding.UTF8.GetBytes(data));

      _logger.LogInformation("Service Bus is sending Address Id", data);
      await _queueClient.SendAsync(message);
    }
  }
}
