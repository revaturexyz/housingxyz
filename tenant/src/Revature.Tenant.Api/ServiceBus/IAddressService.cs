using Revature.Tenant.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Revature.Tenant.Api
{
  public interface IAddressService
  {
    public Task CreateAddressAsync(ApiAddress item);
  }
}
