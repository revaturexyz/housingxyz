using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Revature.Tenant.Api.Models;

namespace Revature.Tenant.Api
{
  /// <summary>
  /// Sends a GET request to the Address service to check if there is a duplicate.
  /// </summary>

  public class AddressClient
  {
    //add config and logger and constructor here
    //public async Task<ActionResult<bool>> GetAddressAsync(ApiAddress apiAddress)
    //{
    //  // Call asynchronous network methods in a try/catch block to handle exceptions.
    //  try
    //  {
    //    HttpResponseMessage response = await AddressClient.GetAddressAsync(configuration["AddressService"]);
    //    response.EnsureSuccessStatusCode();
    //    string responseBody = await response.Content.ReadAsStringAsync();
    //    // Above three lines can be replaced with new helper method below
    //    // string responseBody = await client.GetStringAsync(uri);

    //    Console.WriteLine(responseBody);
    //  }
    //  catch (HttpRequestException e)
    //  {
    //    Console.WriteLine("\nException Caught!");
    //    Console.WriteLine("Message :{0} ", e.Message);
    //  }

    //}


  }
}
