using Revature.Complex.Api.Models;
using System;
using System.Threading.Tasks;

namespace Revature.Complex.Api.Services
{
  public interface IAddressRequest
  {
    public Task<ApiComplexAddress> PostAddressAsync(ApiComplexAddress item);

    public Task<ApiComplexAddress> GetAddressAsync(Guid addressId);
  }
}
