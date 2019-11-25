using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revature.Complex.Api.Services
{
  public interface IRoomServiceReceiver
  {
    abstract public void RegisterOnMessageHandlerAndReceiveMessages();

    abstract public Task CloseQueueAsync();
  }
}
