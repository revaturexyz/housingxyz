using Microsoft.EntityFrameworkCore;
using Revature.Tenant.DataAccess.Entities;
using Revature.Tenant.Lib.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revature.Tenant.DataAccess.Repository
{
  public class TenantRoomRepository : ITenantRoomRepository
  {
    private readonly TenantContext _context;
    private readonly IMapper _map;

    public TenantRoomRepository(TenantContext context, IMapper map)
    {
      _context = context;
      _map = map;
    }

    /// <summary>
    /// Method that gets tenant and associated information of occupants in a room given RoomId
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    public async Task<IList<Lib.Models.Tenant>> GetTenantsByRoomId(Guid roomId)
    {
      var tenants = _context.Tenant
        .Include(t => t.Batch)
        .Include(t => t.Car)
        .Where(t => t.RoomId == roomId);
      return await tenants.Select(t => _map.MapTenant(t)).ToListAsync();
    }
  }
}
