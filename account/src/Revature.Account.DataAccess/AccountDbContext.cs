using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Revature.Account.DataAccess.Entities;

namespace Revature.Account.DataAccess
{
  public class AccountDbContext : DbContext
  {
    public AccountDbContext()
    { }
    public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
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
        entity.Property(e => e.TrainingName)
            .IsRequired()
            .HasMaxLength(100);
        entity.Property(e => e.TrainingAddress)
            .IsRequired()
            .HasMaxLength(100);
        entity.HasMany(e => e.Notifications)
            .WithOne(n => n.Coordinator)
            .HasForeignKey(p => p.NotificationId);
      });

      modelBuilder.Entity<Notification>(entity =>
      {
        entity.HasKey(e => e.NotificationId);
        entity.Property(e => e.ProviderId)
            .IsRequired();
        entity.Property(e => e.CoordinatorId)
            .IsRequired();
        entity.Property(e => e.Status)
            .IsRequired()
            .HasMaxLength(100);
        entity.Property(e => e.AccountExpire)
            .IsRequired();
        entity.Property(e => e.Provider)
            .IsRequired();
        entity.HasOne(e => e.Coordinator)
            .WithMany(n => n.Notifications)
            .HasForeignKey(p => p.CoordinatorId)
            .IsRequired();
      });
    }
  }
}
