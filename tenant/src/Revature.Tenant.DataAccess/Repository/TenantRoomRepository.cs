using Microsoft.EntityFrameworkCore;
using Revature.Tenant.DataAccess.Entities;
using Revature.Tenant.Lib.Interface;
using Revature.Tenant.Lib.Models;
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
    /// Returns a list of tenants that are not currently assigned to a room.
    /// </summary>
    /// <returns></returns>
    /// <remarks>Batch needed for the language and batch end dates utilized in filtering the rooms</remarks>
    public async Task<IList<Lib.Models.Tenant>> GetRoomlessTenantsAsync()
    {
      var tenants = await _context.Tenant
        .Include(t => t.Batch)
        .Where(t => t.RoomId == null)
        .ToListAsync();
      return tenants.Select(t=>_map.MapTenant(t)).ToList();
    }

    /// <summary>
    /// Method that gets tenant and associated information of occupants in a room given RoomId
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    /// <remarks>Batch needed for the language and batch end dates utilized in filtering the rooms</remarks>
    public async Task<List<Lib.Models.Tenant>> GetTenantsByRoomIdAsync(Guid roomId)
    {
      var tenants = await _context.Tenant
        .Include(t => t.Batch)
        .Where(t => t.RoomId == roomId)
        .ToListAsync();
      return tenants.Select(t => _map.MapTenant(t)).ToList();
    }


  }
}
