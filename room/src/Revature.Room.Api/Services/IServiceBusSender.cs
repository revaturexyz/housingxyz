using Revature.Room.Lib;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceBusMessaging
{
  public interface IServiceBusSender
  {
    Task SendDeleteComplexMessage(List<Guid> roomToSend);

    Task SendDeleteMessage(Guid roomToSend);
  }
}
