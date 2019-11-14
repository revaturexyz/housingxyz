using System;
using System.Collections.Generic;
using System.Linq;
using Xyz.Address.Dbx.Models;
using Domain = Xyz.Address.Lib.Models;

namespace Xyz.Address.Dbx.Repositories
{
    ///<summary> Create concrete address entity for Data Repository Pattern. Implements CRUD Operations defined in IRepository. </summary>
    public class AddressRepository: IRepository
    {
      private AddressDbContext _db;

      #region Constructor
      public AddressRepository(AddressDbContext context){
        _db = context;
      }

      #endregion

      #region Method

      ///<summary> Create an address in the DB. </summary>
      ///<param name="newAddy"> newAddy </param>
      ///<return> True (If creation was successful, otherwise false) </return>
      public bool Create(AddressEntity newAddy)
      {
        try
        {
          if(newAddy.Key.ToString() == "00000000-0000-0000-0000-000000000000"){
            newAddy.Key = Guid.NewGuid();
          }
          _db.Addresses.Add(newAddy);
          _db.SaveChanges();
        }
        catch
        {
          return false;
        }
        return true;
      }

      ///<summary> Return a Domain Address Model given an Address key. </summary>
      ///<param name="Key"> Key </param>
      ///<return> Domain.AddressModel </return>
      public Domain.AddressModel Read(Guid Key)
      {
        try
        {
          return _db.Addresses.Single(addentity => addentity.Key == Key) as Domain.AddressModel;
        }
        catch
        {
          return null;
        }
      }

      ///<summary> Return a list of Addresses from the DB. </summary>
      ///<return> List of Domain Address Models </return>
      public IEnumerable<Domain.AddressModel> ReadAll(){
        var newList = new List<Domain.AddressModel>();
        try
        {
          var listOfAddressEntities = _db.Addresses.ToList();
          foreach (var AddressEntityObject in listOfAddressEntities)
          {
            newList.Add(AddressEntityObject as Domain.AddressModel);
          }
        }
        catch
        {
          newList.Clear();
        }
        return newList;
      }

      ///<summary> Update an Address' information in the DB. </summary>
      ///<param name="newAddy"> newAddy </param>
      ///<return> True (If update was successful, otherwise false with an exception)</return>
      public bool Update(AddressEntity newAddy)
      {
        try
        {
          var setUpdate = _db.Addresses.Single(u => u.Key == newAddy.Key); 
          setUpdate.DateModified = DateTime.Now;
          setUpdate.Street = newAddy.Street;
          setUpdate.City = newAddy.City;
          setUpdate.StateProvince = newAddy.StateProvince;
          setUpdate.Country = newAddy.Country;
          setUpdate.PostalCode = newAddy.PostalCode;
          _db.Addresses.Update(setUpdate);
          _db.SaveChanges();
        }
        catch
        {
          return false;
        }
        return true;
      }

      ///<summary> Set an address in the DB as inactive by updating "Active" column </summary>
      ///<param name="Key"> Key </param>
      ///<return> True (if deactivation was successful, otherwise false) </return>
      public bool Delete(Guid Key)
      {
        try
        {
          var setDeactive = _db.Addresses.Single(s => s.Key == Key);
          setDeactive.Active = false;
          setDeactive.DateModified = DateTime.Now;
          _db.Addresses.Update(setDeactive);
          _db.SaveChanges();
        }
        catch
        {
          return false;
        }
        return true;
      }
    }

    #endregion
}
