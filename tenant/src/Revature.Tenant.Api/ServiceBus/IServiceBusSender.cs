using System;
using System.Threading.Tasks;
using Revature.Tenant.Lib.Models;

namespace Revature.Tenant.Api.ServiceBus
{
  public interface IServiceBusSender
  {
    Task SendRoomIdMessage(Guid roomId);
    Task SendAddressIdMessage(Models.ApiAddress address);
  }
}
