using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revature.Address.Api.ServiceBus
{
  interface IServiceBusSender
  {
    public Task SendMessage(Revature.Address.Lib.Address sentAddress);
  }
}
