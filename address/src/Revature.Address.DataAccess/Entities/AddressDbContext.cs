using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Revature.Address.DataAccess.Entities
{
  /// <summary>
  /// 
  /// </summary>
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
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Street).IsRequired();
        entity.Property(e => e.City).IsRequired();
        entity.Property(e => e.State).IsRequired();
        entity.Property(e => e.Country).IsRequired();
        entity.Property(e => e.ZipCode).IsRequired().HasMaxLength(5);
      });
    }
  }
}
