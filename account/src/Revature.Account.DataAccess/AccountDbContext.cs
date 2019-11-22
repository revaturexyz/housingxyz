using Microsoft.EntityFrameworkCore;
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
    public virtual DbSet<UpdateAction> UpdateAction { get; set; }
    public virtual DbSet<Status> Status { get; set; }
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
        entity.HasOne(e => e.Status);
        entity.Property(e => e.AccountCreatedAt)
          .IsRequired();
      });

      modelBuilder.Entity<CoordinatorAccount>(entity =>
      {
        entity.HasKey(e => e.CoordinatorId);
        entity.Property(e => e.Name)
          .IsRequired()
          .HasMaxLength(100);
        entity.Property(e => e.TrainingCenterName)
          .IsRequired()
          .HasMaxLength(100);
        entity.Property(e => e.TrainingCenterAddress)
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
        entity.Property(e => e.UpdateActionId)
          .IsRequired();
        entity.Property(e => e.AccountExpiresAt)
          .IsRequired();
        entity.HasOne(e => e.Coordinator)
          .WithMany(n => n.Notifications)
          .HasForeignKey(p => p.CoordinatorId)
          .IsRequired();
        entity.HasOne(e => e.UpdateAction);
      });

      modelBuilder.Entity<Status>(entity =>
      {
        entity.HasKey(e => e.StatusId);
        entity.Property(e => e.StatusText)
          .IsRequired();
        entity.HasData(
          new Status()
          {
            StatusId = 1,
            StatusText = "Pending"
          },
          new Status()
          {
            StatusId = 2,
            StatusText = "Approved"
          },
          new Status()
          {
            StatusId = 3,
            StatusText = "Rejected"
          },
          new Status()
          {
            StatusId = 4,
            StatusText = "Under Review"
          }
        );
      });
    }
  }
}
