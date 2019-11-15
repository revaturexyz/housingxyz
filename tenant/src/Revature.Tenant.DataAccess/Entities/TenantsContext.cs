using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

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
      builder.Entity<Tenants>().HasKey(t => t.Id);
      builder.Entity<Tenants>().Property(t => t.Email).IsRequired();
      builder.Entity<Tenants>().Property(t => t.Gender).IsRequired();
      builder.Entity<Tenants>().Property(t => t.FirstName).IsRequired().HasMaxLength(60);
      builder.Entity<Tenants>().Property(t => t.LastName).IsRequired().HasMaxLength(60);

      builder.Entity<Cars>().HasKey(c => c.Id);
      builder.Entity<Cars>().Property(c => c.LicensePlate).IsRequired().HasMaxLength(75);
      builder.Entity<Cars>().Property(c => c.Make).IsRequired().HasMaxLength(75);
      builder.Entity<Cars>().Property(c => c.Model).IsRequired().HasMaxLength(75);
      builder.Entity<Cars>().Property(c => c.Color).IsRequired().HasMaxLength(75);
      builder.Entity<Cars>().Property(c => c.Year).IsRequired();
    }
  }
}
