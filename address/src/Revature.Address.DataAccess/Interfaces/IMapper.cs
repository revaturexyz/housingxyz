using System;
using System.Collections.Generic;
using System.Text;
using Revature.Address.Lib;

namespace Revature.Address.DataAccess.Interfaces
{
  /// <summary>
  ///             brief description here
  /// </summary>
  public interface IMapper
  {
    public Revature.Address.Lib.Address MapAddress(Entities.Address address);
    public Entities.Address MapAddress(Revature.Address.Lib.Address address);
  }
}
