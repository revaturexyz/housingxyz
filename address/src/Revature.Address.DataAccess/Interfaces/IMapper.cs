using System;
using System.Collections.Generic;
using System.Text;
using Revature.Address.Lib;

namespace Revature.Address.DataAccess.Interfaces
{
  /// <summary>
  /// Interface for Mapper class to facilitate dependency injection,
  /// Maps DataAccess address objects to Business Library address objects
  /// and vice versa
  /// </summary>
  public interface IMapper
  {

    public Lib.Address MapAddress(Entities.Address address);
    public Entities.Address MapAddress(Lib.Address address);
  }
}
