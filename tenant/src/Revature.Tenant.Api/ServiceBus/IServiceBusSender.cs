using System.Threading.Tasks;
using Revature.Tenant.Lib.Models;

namespace Revature.Tenant.Api.ServiceBus
{
  public interface IServiceBusSender
  {
    Task SendMessage(Lib.Models.Tenant tenantToSend);
  }
}
