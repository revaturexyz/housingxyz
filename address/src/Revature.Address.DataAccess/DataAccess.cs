using Microsoft.EntityFrameworkCore;
using Revature.Address.DataAccess.Entities;
using Revature.Address.DataAccess.Interfaces;
using Revature.Address.Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Revature.Address.DataAccess
{
  /// <summary>
  /// Contain methods for inserting, retrieving, and deleting
  /// information from the database
  /// </summary>
  public class DataAccess : IDataAccess
  {
    private readonly IMapper _mapper;
    private readonly AddressDbContext _context;
    private readonly ILogger _logger;

    /// <summary>
    /// constructor for the project0 database access
    /// </summary>
    /// <param name="context">Dbcontext for accessing the database</param>
    public DataAccess(AddressDbContext context, IMapper mapper)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    //public DataAccess(AddressDbContext context)
    //{
    //  _context = context ?? throw new ArgumentNullException(nameof(context));
    //}

    /// <summary>
    /// Checks if address already exists in database,
    /// if it doesn't it converts it and adds it to the database
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    public async Task<bool> AddAddressAsync(Lib.Address address)
    {
      try
      {
        Lib.Address newAddress = (await GetAddressAsync(address: address)).FirstOrDefault();
        if (newAddress == null)
        {
          await _context.AddAsync(_mapper.MapAddress(address));
        }
      }
      catch
      {
        _logger.LogError($"Address Id: {address.Id} failed to add to database");
        return false;
      }
      return true;
    }

    /// <summary>
    /// Given either an id or an address it retrieves an address from the database 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="address"></param>
    /// <returns>Returns an address</returns>
    public async Task<ICollection<Lib.Address>> GetAddressAsync(Guid? id = null, Lib.Address? address = null)
    {
      List<Entities.Address> addresses = await _context.Addresses.AsNoTracking().ToListAsync();

      if (id != null)
        addresses = addresses.Where(a => a.Id == id).ToList();
      if (address != null)
      {
        addresses = addresses.Where(a => a.Street == address.Street && a.City == address.City && a.State == address.State && a.ZipCode == address.ZipCode && a.Country == address.Country).ToList();
      }
      return addresses.Select(_mapper.MapAddress).ToList();
    }

    /// <summary>
    /// Given an id, it deletes the corresponding address from the database
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Returns true or false</returns>
    public async Task<bool> DeleteAddressAsync(Guid? id)
    {
      if (await _context.Addresses.FindAsync(id) is Entities.Address item)
      {
        _context.Addresses.Remove(item);
        return true;
      }
      return false;
    }

    /// <summary>
    /// Saves changes made to the database
    /// </summary>
    /// <returns></returns>
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
