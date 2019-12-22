using Revature.Complex.Api.Models;
using System.Threading.Tasks;

namespace Revature.Complex.Api.Services
{
  public interface IRoomServiceSender
  {
    /// <summary>
    /// ServiceBus message for sending a message about ApiRoomtoSend model
    /// </summary>
    /// <param name="roomToSend"></param>
    /// <returns></returns>
    public Task SendRoomsMessages(ApiRoomtoSend rooms);
  }
}
