using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Revature.Tenant.Lib.Interface
{
  public interface ITenantRoomRepository
  {
    /// <summary>
    /// Method that gets tenant and associated information of occupants in a room given RoomId
    /// </summary>
    /// <param name="roomId">Room Id of a room</param>
    /// <returns></returns>
    public Task<List<Models.Tenant>> GetTenantsByRoomId(Guid roomId);
    /// <summary>
    /// Method that gets tenants with no room assignments
    /// </summary>
    /// <returns></returns>
    public Task<IList<Models.Tenant>> GetRoomlessTenants();
  }
}
