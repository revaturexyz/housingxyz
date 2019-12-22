using Revature.Complex.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Revature.Complex.Api.Services
{
  public interface IRoomRequest
  {
    public Task<IEnumerable<ApiRoom>> GetRooms(Guid complexId);
  }
}
