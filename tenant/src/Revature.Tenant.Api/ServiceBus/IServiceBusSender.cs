using Revature.Tenant.Lib.Models;
using System.Threading.Tasks;

namespace Revature.Tenant.Api.ServiceBus
{
  public interface IServiceBusSender
  {
    /// <summary>
    /// ServiceBus message for sending a tenant room id
    /// </summary>
    /// <param name="roomId">The GUID of a room</param>
    Task SendRoomIdMessage(RoomMessage roomMessage);
  }
}
