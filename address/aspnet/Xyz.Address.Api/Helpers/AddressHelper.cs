using System;
using System.Collections.Generic;
using Xyz.Address.Lib.Models;
using Xyz.Address.Dbx.Models;
using Xyz.Address.Dbx.Repositories;


namespace Xyz.Address.Api.Helpers
{
  ///<summary> Available HTTP methods used for the Address service. </summary>
  public class AddressHelper
  {
    ///<summary> The following three lines are part of Singleton Pattern. 
    ///Reasoning: Ensures there is one instance of AddressHelper per API request per person.</summary>
    private static AddressHelper _helper;
    private static IRepository _repository;
    private AddressHelper() {}
    
    #region Methods

    ///<summary> HTTP Get: Return list of addresses based on AddressRepository ReadAll Method. </summary>
    ///<return> Collection of addresses.  </return>
    public IEnumerable<AddressModel> Get()
    {
      return _repository.ReadAll();
    }

    ///<summary> HTTP Get: Return address by passing Guid Key to AddressRepository Read Method. </summary>
    ///<param name="g"> Guid Key. </param>
    ///<return> Address.  </return>
    public AddressModel Get(Guid g)
    {
      return _repository.Read(g);
    }

    ///<summary> Take an AddressModel and pass an AddressEntity to AddressRepository Create Method. </summary>
    ///<param name="Am"> AddressModel </param>
    ///<return> True or False  </return>
    public bool Post(AddressModel Am)
    {
      return _repository.Create(Am as AddressEntity);
    }

    ///<summary> Take an AddressModel and pass an AddressEntity to AddressRepository Update Method. </summary>
    ///<param name="Am"> AddressModel </param>
    ///<return> True or False  </return>
    public bool Put(AddressModel Am)
    {
      return _repository.Update(Am as AddressEntity);
    }

    ///<summary> Given a key, set a corresponding address as inactive. </summary>
    ///<param name="key"> Key </param>
    ///<return> True (The deletion was successful, false othewise) </return>
    public bool Delete(Guid key){
      return _repository.Delete(key);
    }

    ///<summary>This method is also part of the Singleton Pattern </summary>
    public static AddressHelper Instance()
    {
      if (_helper is null && _repository is null)
      {
        _helper = new AddressHelper();
        _repository = new AddressRepository(new Dbx.AddressDbContext());
      }

      return _helper;
    }

    public static AddressHelper ChangeRepository(Dbx.AddressDbContext _new){
      _helper = new AddressHelper();
      _repository = new AddressRepository(_new);
      return _helper;
    }
    #endregion
  }
}
