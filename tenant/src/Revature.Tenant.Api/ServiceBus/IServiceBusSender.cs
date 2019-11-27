using System;
using System.Threading.Tasks;

namespace Revature.Tenant.Api.ServiceBus
{
  public interface IServiceBusSender
  {
    Task SendRoomIdMessage(Guid roomId);
  }
}
