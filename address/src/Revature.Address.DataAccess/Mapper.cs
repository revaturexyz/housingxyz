using Revature.Address.DataAccess.Interfaces;

namespace Revature.Address.DataAccess
{
  /// <summary>
  /// Handles mapping between business library and data access address objects
  /// </summary>
  public class Mapper : IMapper
  {
    public Mapper() { }

    /// <summary>
    /// Converts DataAccess address object into Business Library address object
    /// </summary>
    /// <param name="address"></param>
    /// <returns>Returns Business Library address object</returns>
    public Lib.Address MapAddress(Entities.Address address)
    {
      return new Lib.Address
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
    /// Converts Business Library address object into DataAccess address object 
    /// </summary>
    /// <param name="address"></param>
    /// <returns>Returns DataAccess address object</returns>
    public Entities.Address MapAddress(Lib.Address address)
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
