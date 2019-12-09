using System.Threading.Tasks;
using Revature.Tenant.Api.Models;

namespace Revature.Tenant.Api
{
  public interface IAddressService
  {
    /// <summary>
    /// Gets the ID of an address in Address Service - if the address does not already exist, address service can use
    /// the address sent in the query string to Post a new address. The official Address entry will always accopany a success response.
    /// </summary>
    /// <param name="item">A model of an Address</param>
    /// <returns>A model of the formal Address entry in Address Services Database, including it GUID</returns>
    public Task<ApiAddress> GetAddressAsync(ApiAddress item);
  }
}
