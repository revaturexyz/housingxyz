using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xyz.AccountService.DataAccess.Entities;

namespace Xyz.AccountService.DataAccess
{
	public class AccountServiceDbContext : DbContext
	{
		public AccountServiceDbContext()
		{ }

		public AccountServiceDbContext(DbContextOptions<AccountServiceDbContext> options) : base(options)
		{
		}

		public virtual DbSet<Notification> Notification { get; set; }
		public virtual DbSet<ProviderAccount> ProviderAccount { get; set; }
		public virtual DbSet<CoordinatorAccount> CoordinatorAccount { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ProviderAccount>(entity => 
			{
				entity.HasKey(e => e.ProviderId);

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(100);

				entity.Property(e => e.Password)
					.IsRequired()
					.HasMaxLength(100);

				entity.Property(e => e.Status)
					.IsRequired();

				entity.Property(e => e.AccountCreated)
					.IsRequired();

				entity.HasOne(e => e.Coordinator)
					.WithOne(p => p.Provider)
					.IsRequired();

				entity.HasMany(e => e.Notification)
					.WithOne(p => p.Provider)
					.IsRequired();

				entity.HasData
				(new Entities.ProviderAccount
				{
					ProviderId = Guid.NewGuid(),
					CoordinatorId = Guid.NewGuid(),
					Name = "Liv+",
					Password = "password",
					Status = "Pending",
					AccountCreated = DateTime.Now,
					Expire = DateTime.Now.AddDays(7)
				});

			});

			modelBuilder.Entity<CoordinatorAccount>(entity => 
			{
				entity.HasKey(e => e.CoordinatorId);

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(100);

				entity.Property(e => e.Password)
					.IsRequired()
					.HasMaxLength(100);

				entity.Property(e => e.TrainingCenterLocation)
					.IsRequired()
					.HasMaxLength(100);

				entity.Property(e => e.TrainingAddress)
					.IsRequired()
					.HasMaxLength(100);

				entity.HasOne(e => e.Provider)
					.WithOne(p => p.Coordinator)
					.IsRequired();

				entity.HasMany(e => e.Notification)
					.WithOne(n => n.Coordinator);

				entity.HasData
				(new Entities.CoordinatorAccount
				{
					CoordinatorId = Guid.NewGuid(),
					ProviderId = Guid.NewGuid(),
					Email = "example@gmail.com",
					Name = "Revature",
					Password = "password",
					TrainingCenterLocation = "Arlington",
					TrainingAddress = "123 Main St, TX, 77075"
				});
			});

			modelBuilder.Entity<Notification>(entity =>
			{
				entity.HasKey(fr => new { fr.ProviderId, fr.CoordinatorId });

				entity.Property(e => e.ProviderId)
					.IsRequired();

				entity.Property(e => e.CoordinatorId)
					.IsRequired();

				entity.Property(e => e.Status)
					.IsRequired()
					.HasMaxLength(100);

				entity.Property(e => e.AccountExpire)
					.IsRequired();

		

				entity.HasData
				(new Entities.Notification 
				{
					ProviderId = Guid.NewGuid(),
					CoordinatorId = Guid.NewGuid(),
					Status = "Under Review",
					AccountExpire = DateTime.Now.AddDays(30)
				});


			});
		}
	}
}
