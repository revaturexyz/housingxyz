using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Revature.Tenant.Api.ServiceBus
{
  public class RoomService : IRoomService
  {
    private readonly HttpClient _client;

    public RoomService(HttpClient client, IConfiguration configuration)
    {
      client.BaseAddress = new Uri(configuration["AppServices:Room"]);
      _client = client;
    }

    public async Task<HttpResponseMessage> GetVacantRoomsAsync(string gender, DateTime endDate)
    {
      string resourceURI = "api/rooms?gender=" + gender + "&endDate=" + endDate;
      var response = await _client.GetAsync(resourceURI);
      return response;
    }
  }
}
