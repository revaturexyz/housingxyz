using Domain = Xyz.Address.Lib.Models;
using Data = Xyz.Address.Dbx.Models;
using System;

namespace Xyz.Address.Api.Models{
  
  ///<summary>Casts AddressApiModel as AddressEntity for API service methods. Inherits from AddressModel.
  ///We create this model here for de-coupling purposes.</summary>
  ///<return>new Instance of AddressEntity model.</return>
  public class AddressApiModel: Domain.AddressModel{
    public Data.AddressEntity ConvertToEntity(){
      var newAddressEntity = new Data.AddressEntity();
      newAddressEntity.Key = this.Key;
      newAddressEntity.Street = this.Street;
      newAddressEntity.City = this.City;
      newAddressEntity.StateProvince = this.StateProvince;
      newAddressEntity.Active = true;
      newAddressEntity.Country = this.Country;
      newAddressEntity.PostalCode = this.PostalCode;
      return newAddressEntity;
    }
  }
}