using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Revature.Tenant.Api.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Revature.Tenant.Api
{
  /// <summary>
  /// Sends a GET request to the Address service to check if there is a duplicate.
  /// </summary>

  public class AddressClient
  {
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;

    public AddressClient(IConfiguration configuration, ILogger<AddressClient> logger = null)
    {
      _logger = logger;
      _configuration = configuration;
    }
    public async Task<ActionResult<bool>> PostAddressAsync(ApiAddress apiAddress)
    {

      // Call asynchronous network methods in a try/catch block to handle exceptions.
      try
      {
        using var client = new HttpClient { BaseAddress = new Uri(_configuration["AddressService"]) };
        HttpContent address = apiAddress;
        HttpResponseMessage response = await client.PostAsync("", apiAddress);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        // Above three lines can be replaced with new helper method below
        // string responseBody = await client.GetStringAsync(uri);

        Console.WriteLine(responseBody);
      }
      catch (HttpRequestException e)
      {
        Console.WriteLine("\nException Caught!");
        Console.WriteLine("Message :{0} ", e.Message);
      }

    }


  }
}
