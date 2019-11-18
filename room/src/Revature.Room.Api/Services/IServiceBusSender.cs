using System.Collections.Generic;
using System.Threading.Tasks;
using Revature.Room.Lib;

namespace ServiceBusMessaging
{
  public interface IServiceBusSender
  {
    Task SendMessage(Room roomToSend);

    Task SendCreateMessage(Room roomToSend);

    Task SendReadMessage(Room roomToSend);

    Task SendReadMessage(IEnumerable<Room> roomListNull);

    Task SendUpdateMessage(Room roomToSend);

    Task SendDeleteMessage(Room roomToSend);
  }
}
