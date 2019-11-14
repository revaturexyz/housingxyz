using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Xyz.Address.Dbx.Models;
using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xyz.Address.Dbx
{

  /// <summary>
  /// The DbContext is responsible for opening and managing connections to the database. It is the primary class responsible for 
  /// interacting with data as objects DbContext.
  /// </summary>
  public class AddressDbContext : DbContext
  {
    public DbSet<AddressEntity> Addresses {get; set;}
    public AddressDbContext(){}
    public AddressDbContext(DbContextOptions<AddressDbContext> options): base(options){
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder){
      if (!builder.IsConfigured)
      builder.UseNpgsql("Host=db;port=5432;Database=postgres;Username=postgres;Password=Password12345");
    }

    /// <summary> OnModelCreating is a method of DbContext that takes an instance of ModelBuilder as a parameter. </summary>
    /// <param name="builder"></param>
    protected override void OnModelCreating(ModelBuilder builder)
    {

      builder.Entity<AddressEntity>().HasKey(a => a.Id);
      builder.Entity<AddressEntity>().HasIndex(a => a.Key).IsUnique();
      builder.Entity<AddressEntity>().Property(a => a.Street).IsRequired().HasColumnType("varchar").HasMaxLength(50);
      builder.Entity<AddressEntity>().Property(a => a.City).IsRequired().HasColumnType("varchar").HasMaxLength(50);
      builder.Entity<AddressEntity>().Property(a => a.StateProvince).IsRequired().HasColumnType("varchar").HasMaxLength(50);
      builder.Entity<AddressEntity>().Property(a => a.Country).IsRequired().HasColumnType("varchar").HasMaxLength(50);
      builder.Entity<AddressEntity>().Property(a => a.PostalCode).IsRequired().HasColumnType("varchar").HasMaxLength(5);
      base.OnModelCreating(builder);
    }
  }
}
