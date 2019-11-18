using System.Threading.Tasks;
using Revature.Tenant.Lib.Models;

namespace ServiceBusMessaging
{
  public interface IServiceBusSender
  {
    Task SendMessage(Tenant tenantToSend);
  }
}
