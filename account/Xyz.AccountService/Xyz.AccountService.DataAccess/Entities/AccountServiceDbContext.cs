using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Xyz.AccountService.DataAccess.Entities
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

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(60);

				entity.Property(e => e.Password)
					.IsRequired()
					.HasMaxLength(60);

				entity.Property(e => e.Status)
					.IsRequired()
					.HasMaxLength(60);

				entity.Property(e => e.AccountCreated)
					.IsRequired();

				entity.HasOne(e => e.Coordinator)
					.WithOne(p => p.Provider)
					.IsRequired();

				entity.HasMany(e => e.Notification)
					.WithOne(p => p.Provider)
					.IsRequired();

			});

			modelBuilder.Entity<CoordinatorAccount>(entity => 
			{
				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(60);

				entity.Property(e => e.Password)
					.IsRequired()
					.HasMaxLength(60);

				entity.Property(e => e.TrainingName)
					.IsRequired()
					.HasMaxLength(60);

				entity.Property(e => e.TrainingAddress)
					.IsRequired()
					.HasMaxLength(75);

				entity.HasOne(e => e.Provider)
					.WithOne(p => p.Coordinator)
					.IsRequired();

				entity.HasMany(e => e.Notification)
					.WithOne(n => n.Coordinator);
				
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
					.HasMaxLength(60);

				entity.Property(e => e.AccountExpire)
					.IsRequired();


			});
		}
	}
}
