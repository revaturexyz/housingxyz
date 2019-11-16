using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Revature.Complex.DataAccess.Entities
{
  public class ComplexDbContext : DbContext
  {
    public ComplexDbContext() { }

    public ComplexDbContext(DbContextOptions<ComplexDbContext> options)
    : base(options) { }

    public virtual DbSet<Complex> Complex { get; set; }

    public virtual DbSet<AmenityComplex> AmenityComplex { get; set; }
    public virtual DbSet<AmenityRoom> AmenityRoom { get; set; }
    public virtual DbSet<Amenity> Amenity { get; set; }

    public Guid cId1 = Guid.NewGuid();
    public Guid cId2 = Guid.NewGuid();
    public Guid cId3 = Guid.NewGuid();
    public Guid cId4 = Guid.NewGuid();
    public Guid rId1 = Guid.NewGuid();
    public Guid rId2 = Guid.NewGuid();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Complex>(entity =>
      {
        entity.HasKey(e => e.ComplexId);

        entity.Property(e => e.AddressId)
            .IsRequired();

        entity.Property(e => e.ProviderId)
            .IsRequired();

        entity.Property(e => e.ComplexName)
          .IsRequired()
          .HasMaxLength(100);

        entity.Property(e => e.ContactNumber)
          .HasMaxLength(20);

        entity.HasData(
          new Complex
          {
            ComplexId = Guid.NewGuid(),
            AddressId = Guid.NewGuid(),
            ProviderId = Guid.NewGuid(),
            ComplexName = "Liv+",
            ContactNumber = "8177517911"
          },
          new Complex
          {
            ComplexId = cId2,
            AddressId = Guid.NewGuid(),
            ProviderId = Guid.NewGuid(),
            ComplexName = "SampleComplex",
            ContactNumber = "4445550506"
          },
          new Complex
          {
            ComplexId = cId3,
            AddressId = Guid.NewGuid(),
            ProviderId = Guid.NewGuid(),
            ComplexName = "Complex",
            ContactNumber = "7771112222"
          },
          new Complex
          {
            ComplexId = cId4,
            AddressId = Guid.NewGuid(),
            ProviderId = Guid.NewGuid(),
            ComplexName = "ComplexNearMe",
            ContactNumber = "3332221111"
          }
        );

      });

      modelBuilder.Entity<Amenity>(entity =>
      {
        entity.Property(e => e.AmenityId)
            .UseIdentityColumn();

        entity.Property(e => e.AmenityType)
            .IsRequired();

        entity.Property(e => e.Description);

        entity.HasData
        (
            new Amenity { AmenityId = 1, AmenityType = "Test1", Description = "Description1" },
            new Amenity { AmenityId = 2, AmenityType = "Test2", Description = "" },
            new Amenity { AmenityId = 3, AmenityType = "Test3", Description = "Description3" }
        );

      });

      modelBuilder.Entity<AmenityComplex>(entity =>
      {
        entity.Property(e => e.AmenityComplexId)
              .UseIdentityColumn()
              .IsRequired();

        entity.HasIndex(c => c.AmenityComplexId)
              .IsUnique();

        entity.HasOne(e => e.Amenity)
              .WithMany(d => d.AmenityComplex)
              .HasForeignKey(p => p.AmenityId)
              .IsRequired()
              .OnDelete(DeleteBehavior.ClientSetNull);


        entity.HasOne(e => e.Complex)
              .WithMany(d => d.AmentityComplex)
              .HasForeignKey(p => p.ComplexId)
              .IsRequired()
              .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasData
        (
            new AmenityComplex { AmenityComplexId = 1, AmenityId = 1, ComplexId = cId1 },
            new AmenityComplex { AmenityComplexId = 2, AmenityId = 2, ComplexId = cId1 },
            new AmenityComplex { AmenityComplexId = 3, AmenityId = 1, ComplexId = cId2 }
        );

      });

      modelBuilder.Entity<AmenityRoom>(entity =>
      {
        entity.Property(e => e.AmenityRoomId)
              .UseIdentityColumn()
              .IsRequired();

        entity.HasIndex(c => c.AmenityRoomId)
              .IsUnique();

        entity.HasOne(e => e.Amenity)
              .WithMany(d => d.AmenityRoom)
              .HasForeignKey(p => p.AmenityId)
              .IsRequired()
              .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.RoomId)
            .IsRequired();

        entity.HasData
        (
            new AmenityRoom { AmenityRoomId = 1, AmenityId = 3, RoomId = rId1 },
            new AmenityRoom { AmenityRoomId = 2, AmenityId = 2, RoomId = rId1 },
            new AmenityRoom { AmenityRoomId = 3, AmenityId = 3, RoomId = rId2 }
        );

      });
    }
  }
}
