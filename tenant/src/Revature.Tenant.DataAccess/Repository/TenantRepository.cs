using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Revature.Tenant.DataAccess.Entities;
using Revature.Tenant.Lib.Interface;

namespace Revature.Tenant.DataAccess.Repository
{
  public class TenantRepository : ITenantRepository 
  {
    private readonly TenantsContext _context;
    private readonly IMapper _mapper;

    public TenantRepository(TenantsContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task AddAsync(Lib.Models.Tenant tenant)
    {
      Tenants newTenant = _mapper.MapTenant(tenant);

      await _context.Tenants.AddAsync(newTenant);
    }

    public async Task<Lib.Models.Tenant> GetByIdAsync(int id)
    {
      Tenants tenant = await _context.Tenants.Include(t => t.Cars).FirstAsync(t => t.Id == id);

      if (tenant == null)
      {
        throw new ArgumentException();
      }

      return _mapper.MapTenant((tenant));
    }

    public async Task<ICollection<Lib.Models.Tenant>> GetAllAsync()
    {
      List<Tenants> tenants = await _context.Tenants.Include(t => t.Cars).AsNoTracking().ToListAsync();

      return tenants.Select(t => new Lib.Models.Tenant
      {
        Id = t.Id,
        Email = t.Email,
        Gender = t.Gender,
        FirstName = t.FirstName,
        LastName = t.LastName,
        AddressId = t.AddressId,
        RoomId = t.RoomId,
        CarId = t.CarId,
        Car = new Lib.Models.Car
        {
          Id = t.Cars.Id,
          LicensePlate = t.Cars.LicensePlate,
          Make = t.Cars.Make,
          Model = t.Cars.Model,
          Color = t.Cars.Color,
          Year = t.Cars.Year,
        },
      }).ToList();
    }

    public async Task DeleteByIdAsync(int id)
    {
      Tenants tenant = await _context.Tenants.FindAsync(id);

      _context.Remove(tenant);
    }

    public async Task UpdateAsync(Lib.Models.Tenant tenant)
    {
      Tenants currentTenant = await _context.Tenants.FindAsync(tenant.Id);

      if (currentTenant == null)
      {
        throw new InvalidOperationException("Invalid Tenant Id");
      }

      Tenants newTenant = _mapper.MapTenant(tenant);
      _context.Entry(currentTenant).CurrentValues.SetValues(newTenant);
    }

    public async Task SaveAsync()
    {
      await _context.SaveChangesAsync();
    }

    #region IDisposable Support
    private bool _disposedValue = false; // To detect redundant calls

    protected virtual void Dispose(bool disposing)
    {
      if (!_disposedValue)
      {
        if (disposing)
        {
          _context.Dispose();
        }

        _disposedValue = true;
      }
    }

    // This code added to correctly implement the disposable pattern.
    public void Dispose()
    {
      Dispose(true);
    }
    #endregion
  }
}
