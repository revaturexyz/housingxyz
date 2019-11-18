using System.Threading.Tasks;
using Revature.Room.Lib;

namespace ServiceBusMessaging
{
  public interface IServiceBusSender
  {
    Task SendMessage(Room roomToSend);
  }
}