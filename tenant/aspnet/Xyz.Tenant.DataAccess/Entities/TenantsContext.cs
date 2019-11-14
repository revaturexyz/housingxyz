using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Xyz.Tenant.DataAccess.Entities
{
  public class TenantsContext : DbContext
  {
    public TenantsContext() { }

    public TenantsContext(DbContextOptions<TenantsContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Tenants>().HasKey(t => t.Id);
      builder.Entity<Tenants>().Property(t => t.FirstName).IsRequired().HasMaxLength(60);
      builder.Entity<Tenants>().Property(t => t.LastName).IsRequired().HasMaxLength(60);
    }
  }
}
