using System;
using Xyz.Address.Dbx.Models;
using System.Collections.Generic;
using Domain = Xyz.Address.Lib.Models;


namespace Xyz.Address.Dbx.Repositories
{
  ///<summary>Methods (CRUD Operations) for the Data Repository Pattern.</summary>
    public interface IRepository
    {
      bool Create(AddressEntity newAddy);
      Domain.AddressModel Read(Guid Key);
      IEnumerable<Domain.AddressModel> ReadAll();
      bool Update(AddressEntity newAddy);
      bool Delete(Guid Key);
    }
}
