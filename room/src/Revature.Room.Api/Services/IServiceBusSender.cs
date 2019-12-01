using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceBusMessaging
{
  /// <summary>
  /// Interface for sending messages via service bus
  /// </summary>
  public interface IServiceBusSender
  {
    /// <summary>
    /// Method that sends a message to complex service to alert it that the room/s have been deleted
    /// </summary>
    /// <param name="roomToSend"></param>
    /// <returns></returns>
    Task SendDeleteMessage(Guid roomToSend);
  }
}
