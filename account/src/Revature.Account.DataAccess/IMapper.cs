
namespace Revature.Account.DataAccess
{
  public interface IMapper
  {
    Lib.Model.CoordinatorAccount MapCoordinator(Entities.CoordinatorAccount coordinator);
    Entities.CoordinatorAccount MapCoordinator(Lib.Model.CoordinatorAccount coordinator);
    Lib.Model.Notification MapNotification(Entities.Notification nofi);
    Entities.Notification MapNotification(Lib.Model.Notification nofi);
    Lib.Model.ProviderAccount MapProvider(Entities.ProviderAccount provider);
    Entities.ProviderAccount MapProvider(Lib.Model.ProviderAccount provider);
    Lib.Model.UpdateAction MapUpdateAction(Entities.UpdateAction action);
    Entities.UpdateAction MapUpdateAction(Lib.Model.UpdateAction action);
  }
}