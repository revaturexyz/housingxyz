using Microsoft.Extensions.Configuration;
using Revature.Tenant.Api.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Revature.Tenant.Api.ServiceBus
{
  public class AddressService : IAddressService
  {
    private readonly HttpClient _client;

    private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public AddressService(HttpClient client, IConfiguration configuration)
    {
      client.BaseAddress = new Uri(configuration.GetSection("AppServices")["Address"]);
      client.DefaultRequestHeaders.Add("Accept", "application/json");

      _client = client;
    }

    public async Task CreateAddressAsync(ApiAddress item)
    {
      using var response = await SendRequestAsync(HttpMethod.Post, "api/Address", item);
      response.EnsureSuccessStatusCode();

      var newItem = await ReadResponseBodyAsync<ApiAddress>(response);
    }

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

    private async Task<T> ReadResponseBodyAsync<T>(HttpResponseMessage response)
    {
      using var stream = await response.Content.ReadAsStreamAsync();
      return await JsonSerializer.DeserializeAsync<T>(stream, _jsonOptions);
    }
  }
}
