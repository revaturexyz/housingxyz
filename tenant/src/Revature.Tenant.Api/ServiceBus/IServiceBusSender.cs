using Revature.Tenant.Lib.Models;
using System.Threading.Tasks;

namespace Revature.Tenant.Api.ServiceBus
{
  public interface IServiceBusSender
  {
    /// <summary>
    /// ServiceBus message for sending a tenant room id
    /// </summary>
    /// <param name="roomMessage">The details room service needs to be able to update a room</param>
    Task SendRoomIdMessage(RoomMessage roomMessage);
  }
}
