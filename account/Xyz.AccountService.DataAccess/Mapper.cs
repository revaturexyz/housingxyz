using System;
using System.Collections.Generic;
using System.Text;

namespace Xyz.AccountService.DataAccess
{
	public class Mapper
	{
		public static Lib.Model.ProviderAccount MapProviders(Entities.ProviderAccount provider)
		{
			return new Lib.Model.ProviderAccount
			{
				ProviderId = provider.ProviderId,
				CoordinatorId = provider.CoordinatorId,
				Name = provider.Name,
				Password = provider.Password,
				Status = provider.Status,
				AccountCreated = provider.AccountCreated,
				Expire = provider.Expire,
			};
		}
		public static Entities.ProviderAccount MapProvider(Lib.Model.ProviderAccount provider)
		{
			return new Entities.ProviderAccount
			{
				ProviderId = provider.ProviderId,
				CoordinatorId = provider.CoordinatorId,
				Name = provider.Name,
				Password = provider.Password,
				Status = provider.Status,
				AccountCreated = provider.AccountCreated,
				Expire = provider.Expire,
			};
		}

		public static Lib.Model.CoordinatorAccount MapCoordinator(Entities.CoordinatorAccount coordinator)
		{
			return new Lib.Model.CoordinatorAccount
			{
				CoordinatorId = coordinator.CoordinatorId,
				Name = coordinator.Name,
				Password = coordinator.Password,
				TrainingName = coordinator.TrainingName,
				TrainingAddress = coordinator.TrainingAddress,
				Email = coordinator.Email
			};
		}
		public static Entities.CoordinatorAccount MapCoordinator(Lib.Model.CoordinatorAccount coordinator)
		{
			return new Entities.CoordinatorAccount
			{
				CoordinatorId = coordinator.CoordinatorId,
				Name = coordinator.Name,
				Password = coordinator.Password,
				TrainingName = coordinator.TrainingName,
				TrainingAddress = coordinator.TrainingAddress,
				Email = coordinator.Email
			};
		}

		public static Lib.Model.Notification MapNotification(Entities.Notification nofi)
		{
			return new Lib.Model.Notification
			{
				ProviderId = nofi.ProviderId,
				CoordinatorId = nofi.CoordinatorId,
				Status = nofi.Status,
				AccountExpire = nofi.AccountExpire
			};
		}
		public static Entities.Notification MapNotification(Lib.Model.Notification nofi)
		{
			return new Entities.Notification
			{
				ProviderId = nofi.ProviderId,
				CoordinatorId = nofi.CoordinatorId,
				Status = nofi.Status,
				AccountExpire = nofi.AccountExpire
			};
		}
	}
}
