using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revature.Address.Api.ServiceBus
{
  /// <summary>
  /// Interface for ServiceBusSender
  /// </summary>
  interface IServiceBusSender
  {
    public Task SendCreateMessage(Lib.Address sentAddress);
    public Task SendDeleteMessage(Lib.Address sentAddress);
  }
}
