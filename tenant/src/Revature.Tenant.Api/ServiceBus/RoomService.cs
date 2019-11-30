using Microsoft.Extensions.Configuration;
using Revature.Tenant.Lib.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Revature.Tenant.Api.ServiceBus
{
  /// <summary>
  /// Service that communicates with the room service
  /// </summary>
  public class RoomService : IRoomService
  {
    private readonly HttpClient _client;

    public RoomService(HttpClient client, IConfiguration configuration)
    {
      client.BaseAddress = new Uri(configuration["AppServices:Room"]);
      _client = client;
    }
    /// <summary>
    /// Method that gets vacant rooms from the room service
    /// </summary>
    /// <param name="gender"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    /// <exception cref="HttpRequestException">Thrown when the response from the room service isn't successful</exception>
    public async Task<List<AvailRoom>> GetVacantRoomsAsync(string gender, DateTime endDate)
    {
      string resourceURI = "api/rooms?gender=" + gender + "&endDate=" + endDate;
      using var response = await _client.GetAsync(resourceURI);
      if(response.IsSuccessStatusCode)
      {
        var contentAsString = await response.Content.ReadAsStringAsync();
        var availableRooms = JsonSerializer.Deserialize<List<AvailRoom>>(contentAsString);
        return availableRooms;
      } else
      {
        throw new HttpRequestException("Unable to receive response from room service");
      }
      
    }
  }
}
