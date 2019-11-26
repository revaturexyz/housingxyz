using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceBusMessaging
{
  public interface IServiceBusSender
  {
    Task SendDeleteMessage(List<Guid> roomToSend);
  }
}
