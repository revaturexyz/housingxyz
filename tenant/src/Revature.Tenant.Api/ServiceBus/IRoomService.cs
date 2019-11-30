using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Revature.Tenant.Api.ServiceBus
{
  public interface IRoomService
  {
    Task<HttpResponseMessage> GetVacantRoomsAsync(string gender, DateTime endDate);
  }
}