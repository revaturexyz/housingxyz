using Microsoft.EntityFrameworkCore;
using Revature.Address.DataAccess.Entities;
using Revature.Address.DataAccess.Interfaces;
using Revature.Address.Lib.Interfaces;
using Revature.Address.Lib;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Revature.Address.DataAccess
{
    public class DataAccess : IDataAccess
    {
    private readonly IMapper _mapper;
    private readonly AddressDbContext _context;

    /// <summary>
    /// constructor for the project0 database access
    /// </summary>
    /// <param name="context">Dbcontext for accessing the database</param>
    public DataAccess(AddressDbContext context, IMapper mapper)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task AddAddressAsync(Revature.Address.Lib.Address address)
    {
      Revature.Address.Lib.Address newAddress = (await GetAddressesAsync(address: address)).FirstOrDefault();
      if (newAddress == null)
      {
        await _context.AddAsync(_mapper.MapAddress(address));
      }
    }

    public async Task<ICollection<Revature.Address.Lib.Address>> GetAddressesAsync(Guid? id = null, Revature.Address.Lib.Address address = null)
    {
      List<Revature.Address.DataAccess.Entities.Address> addresses = await _context.Addresses.AsNoTracking().ToListAsync();

      if (id != null)
        addresses = addresses.Where(a => a.Id == id).ToList();
      if (address != null)
      {
        addresses = addresses.Where(a => a.Street == address.Street && a.City == address.City && a.State == address.State && a.ZipCode == address.ZipCode).ToList();
      }
      return addresses.Select(_mapper.MapAddress).ToList();
    }

    public async Task<bool> DeleteAddressAsync(Guid id)
    {
      if (await _context.Addresses.FindAsync(id) is Entities.Address item)
      {
        _context.Addresses.Remove(item);
        await _context.SaveChangesAsync();
        return true;
      }
      return false;
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
      GC.SuppressFinalize(this);
    }
    #endregion
  }
}
