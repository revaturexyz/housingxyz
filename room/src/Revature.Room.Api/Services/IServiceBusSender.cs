using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Revature.Room.Lib;

namespace ServiceBusMessaging
{
  public interface IServiceBusSender
  {
    Task SendCreateMessage(Room roomToSend);

    Task SendUpdateMessage(Room roomToSend);

    Task SendDeleteMessage(Room roomToSend);

  }
}
