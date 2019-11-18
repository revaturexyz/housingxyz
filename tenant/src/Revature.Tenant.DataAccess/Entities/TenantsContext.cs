using Microsoft.EntityFrameworkCore;

namespace Revature.Tenant.DataAccess.Entities
{
  public class TenantsContext : DbContext
  {
    public TenantsContext() { }

    public TenantsContext(DbContextOptions<TenantsContext> options) : base(options) { }

    public virtual DbSet<Tenants> Tenants { get; set; }
    public virtual DbSet<Cars> Cars { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Tenants>(entity =>
      {
        entity.HasKey(t => t.Id);
        entity.Property(t => t.Email).IsRequired();
        entity.Property(t => t.Gender).IsRequired();
        entity.Property(t => t.FirstName).IsRequired().HasMaxLength(100);
        entity.Property(t => t.LastName).IsRequired().HasMaxLength(100);
        entity.HasOne(t => t.Cars).WithMany(c => c.Tenants).HasForeignKey(t => t.CarId);
      });

      builder.Entity<Cars>(entity =>
      {
        entity.HasKey(c => c.Id);
        entity.Property(c => c.LicensePlate).IsRequired().HasMaxLength(100);
        entity.Property(c => c.Make).IsRequired().HasMaxLength(100);
        entity.Property(c => c.Model).IsRequired().HasMaxLength(100);
        entity.Property(c => c.Color).IsRequired().HasMaxLength(100);
        entity.Property(c => c.Year).IsRequired();
      });
    }
  }
}
