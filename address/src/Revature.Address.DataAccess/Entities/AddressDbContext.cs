using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Revature.Address.DataAccess.Entities
{
  public partial class AddressDbContext : DbContext
  {
    public AddressDbContext()
    {

    }
    public AddressDbContext(DbContextOptions<AddressDbContext> options) : base(options) { }
    public virtual DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Address>(entity =>
      {
        entity.Property(e => e.Street).IsRequired().HasMaxLength(50);
        entity.Property(e => e.City).IsRequired().HasMaxLength(50);
        entity.Property(e => e.State).IsRequired().HasMaxLength(50);
        entity.Property(e => e.Country).IsRequired().HasMaxLength(50);
        entity.Property(e => e.ZipCode).IsRequired().HasMaxLength(50);
      });
    }
  }
}
