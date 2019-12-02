using Microsoft.Extensions.Configuration;
using Revature.Complex.Api.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Revature.Complex.Api.Services
{
  public class RoomRequest : IRoomRequest
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
    /// </summary>
    /// <param name="client">HTTP Client (dependency injection)</param>
    /// <param name="addressConfiguration">Configuration file with base URI.</param>
    public RoomRequest(HttpClient client, IConfiguration addressConfiguration)
    {
      client.BaseAddress = new Uri(addressConfiguration.GetSection("AppServices")["Room"]);
      client.DefaultRequestHeaders.Add("Accept", "application/json");

      _client = client;
    }

    /// <summary>
    /// Gets the ID of an address in Address Service - if the address does not already exist, address service can use
    /// the address sent in the query string to Post a new address. The official Address entry will always accopany a success response.
    /// </summary>
    /// <param name="item">A model of an Address</param>
    /// <returns>A model of the formal Address entry in Address Services Database, including it GUID</returns>
    public async Task<IEnumerable<ApiRoom>> GetRooms(Guid complexId)
    {
      string queryString = complexId.ToString()
        + "/rooms";

      using var response = await SendRequestAsync<ApiRoom>(HttpMethod.Get, "api/complexes/" + queryString);
      response.EnsureSuccessStatusCode();

      return (await ReadResponseBodyAsync<IEnumerable<ApiRoom>>(response));
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
