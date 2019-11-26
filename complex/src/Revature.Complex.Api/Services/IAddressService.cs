using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Revature.Complex.Api.Models;

namespace Revature.Complex.Api.Services
{
  public interface IAddressService
  {
    /// <summary>
    /// ServiceBus message for sending a message to Address service
    /// </summary>
    /// <param name="roomToSend"></param>
    /// <returns></returns>
    public Task SendRoomsMessages(ApiComplexAddress address);
  }
}
