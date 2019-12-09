using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Revature.Tenant.Api.Models;

namespace Revature.Tenant.Api.ServiceBus
{
  public class AddressService : IAddressService
  {
    private readonly HttpClient _client;

    /// <summary>
    /// Set Json Serialization to Camel Case policy
    /// </summary>
    private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    /// <summary>
    /// Construct Address Service with base URI, Default, and injected HTTP Client
    /// NOTE:   This constructor uses a try catch which will perform the try block if address service is operational,
    ///         but will use dummy data otherwise. This should NOT be allowed into a public deployment!!!
    /// </summary>
    /// <param name="client">HTTP Client (dependency injection)</param>
    /// <param name="addressConfiguration">Configuration file with base URI.</param>
    public AddressService(HttpClient client, IConfiguration addressConfiguration)
    {
      try
      {
        client.BaseAddress = new Uri(addressConfiguration.GetSection("AppServices")["Address"]);
        client.DefaultRequestHeaders.Add("Accept", "application/json");

        _client = client;
      }
      catch
      {
        _client = null;
      }
    }

    /// <summary>
    /// Gets the ID of an address in Address Service - if the address does not already exist, address service can use
    /// the address sent in the query string to Post a new address. The official Address entry will always accopany a success response.
    /// NOTE: This Method uses a try catch that may result in the use of dummy data! Should be removed before public deployment.
    /// </summary>
    /// <param name="item">A model of an Address</param>
    /// <returns>A model of the formal Address entry in Address Services Database, including it GUID</returns>
    public async Task<ApiAddress> GetAddressAsync(ApiAddress item)
    {
      try
      {
        var queryString = "?"
          + "street=" + item.Street + "&"
          + "city=" + item.City + "&"
          + "state=" + item.State + "&"
          + "zipCode=" + item.ZipCode + "&"
          + "country=" + item.Country;

        using var response = await SendRequestAsync<ApiAddress>(HttpMethod.Get, "api/Address" + queryString);
        response.EnsureSuccessStatusCode();

        return await ReadResponseBodyAsync<ApiAddress>(response);
      }
      catch
      {
        item.AddressId = Guid.NewGuid();
        return item;
      }
    }

    /// <summary>
    /// Private helper method for sending a HTTP Request between services
    /// </summary>
    /// <returns>A Request Response</returns>
    private async Task<HttpResponseMessage> SendRequestAsync<T>(HttpMethod method, string uri, T body = null) where T : class
    {
      using var request = new HttpRequestMessage(method, uri);
      if (body is T)
      {
        var json = JsonSerializer.Serialize(body, _jsonOptions);
        var content = new StringContent(json, Encoding.Default, "application/json");
        request.Content = content;
      }
      return await _client.SendAsync(request);
    }

    /// <summary>
    /// A private helper method for interpretting a HTTP Response
    /// </summary>
    /// <returns>A generic typed object that may be included in the body of a response.</returns>
    private async Task<T> ReadResponseBodyAsync<T>(HttpResponseMessage response)
    {
      using var stream = await response.Content.ReadAsStreamAsync();
      return await JsonSerializer.DeserializeAsync<T>(stream, _jsonOptions);
    }
  }
}
