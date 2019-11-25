using Microsoft.EntityFrameworkCore;
using Revature.Account.DataAccess.Entities;

namespace Revature.Account.DataAccess
{
  public class AccountDbContext : DbContext
  {
    /// <summary>
    /// Context class that acts to define the structure of a code-first database.
    /// </summary>
    ///

    // Defualt constructor
    public AccountDbContext()
    { }

    // Constructor with options and iheritance from its parent class.
    public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
    {
    }

    // All tables found in the database defined here
    public virtual DbSet<Notification> Notification { get; set; }
    public virtual DbSet<UpdateAction> UpdateAction { get; set; }
    public virtual DbSet<Status> Status { get; set; }
    public virtual DbSet<ProviderAccount> ProviderAccount { get; set; }
    public virtual DbSet<CoordinatorAccount> CoordinatorAccount { get; set; }

    /// <summary>
    /// Defines the features for each and every table.
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<ProviderAccount>(entity =>
      {
        entity.HasKey(e => e.ProviderId);
        entity.Property(e => e.Name)
          .IsRequired()
          .HasMaxLength(100);
        entity.Property(e => e.StatusText);
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
        entity.Property(e => e.StatusText);
      });
    }
  }
}
