using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Revature.Complex.Api.Models;

namespace Revature.Complex.Api.Services
{
  public interface IAddressService
  {
    public Task SendRoomsMessages(ApiComplexAddress address);
  }
}
