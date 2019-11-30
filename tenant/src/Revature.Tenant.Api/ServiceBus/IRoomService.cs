using Revature.Tenant.Lib.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Revature.Tenant.Api.ServiceBus
{
  /// <summary>
  /// Service that communicates with the Room Service to get vacant rooms
  /// </summary>
  public interface IRoomService
  {
    /// <summary>
    /// Method that gets vacant rooms from the room service
    /// </summary>
    /// <param name="gender"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    /// <exception cref="HttpRequestException">Thrown when the response from the room service isn't successful</exception>
    Task<List<AvailRoom>> GetVacantRoomsAsync(string gender, DateTime endDate);
  }
}
