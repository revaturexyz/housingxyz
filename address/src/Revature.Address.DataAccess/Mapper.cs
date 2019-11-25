using Revature.Address.DataAccess.Interfaces;
using Revature.Address.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Address.DataAccess
{

  /// <summary>
  /// Each method needs a summary of what is NOT mapped and why.
  /// </summary>
  public class Mapper : IMapper
  {
    public Mapper() { }
    public Revature.Address.Lib.Address MapAddress(Entities.Address address)
    {
      return new Revature.Address.Lib.Address
      {
        Id = address.Id,
        Street = address.Street,
        City = address.City,
        State = address.State,
        Country = address.Country,
        ZipCode = address.ZipCode,
      };
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    public Entities.Address MapAddress(Revature.Address.Lib.Address address)
    {
      return new Entities.Address
      {
        Id = address.Id,
        Street = address.Street,
        City = address.City,
        State = address.State,
        Country = address.Country,
        ZipCode = address.ZipCode,
      };
    }
  }
}
