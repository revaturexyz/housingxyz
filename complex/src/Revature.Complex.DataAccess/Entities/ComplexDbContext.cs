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

    private Guid cId1 = Guid.NewGuid();
    private Guid cId2 = Guid.NewGuid();
    private Guid cId3 = Guid.NewGuid();
    private Guid cId4 = Guid.NewGuid();
    private Guid rId1 = Guid.NewGuid();
    private Guid rId2 = Guid.NewGuid();
    private Guid amId1 = Guid.NewGuid();
    private Guid amId2 = Guid.NewGuid();
    private Guid amId3 = Guid.NewGuid();
    private Guid amId4 = Guid.NewGuid();
    private Guid amId5 = Guid.NewGuid();

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
            ComplexId = cId1,
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
            .IsRequired();

        entity.HasIndex(e => e.AmenityId)
            .IsUnique();

        entity.Property(e => e.AmenityType)
            .IsRequired();

        entity.Property(e => e.Description);

        entity.HasData
        (
            new Amenity { AmenityId = amId1, AmenityType = "fridge", Description = "to keep foods fresh" },
            new Amenity { AmenityId = amId2, AmenityType = "microwave", Description = "" },
            new Amenity { AmenityId = amId3, AmenityType = "pool", Description = "swmming" },
            new Amenity { AmenityId = amId4, AmenityType = "kitchen", Description = "cook" },
            new Amenity { AmenityId = amId5, AmenityType = "", Description = "work out" }
        );

      });

      modelBuilder.Entity<AmenityComplex>(entity =>
      {
        entity.Property(e => e.AmenityComplexId)
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
            new AmenityComplex { AmenityComplexId = Guid.NewGuid(), AmenityId = amId1, ComplexId = cId1 },
            new AmenityComplex { AmenityComplexId = Guid.NewGuid(), AmenityId = amId2, ComplexId = cId1 },
            new AmenityComplex { AmenityComplexId = Guid.NewGuid(), AmenityId = amId2, ComplexId = cId2 },
            new AmenityComplex { AmenityComplexId = Guid.NewGuid(), AmenityId = amId4, ComplexId = cId2 }
        );

      });

      modelBuilder.Entity<AmenityRoom>(entity =>
      {
        entity.Property(e => e.AmenityRoomId)
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
            new AmenityRoom { AmenityRoomId = Guid.NewGuid(), AmenityId = amId1, RoomId = rId1 },
            new AmenityRoom { AmenityRoomId = Guid.NewGuid(), AmenityId = amId4, RoomId = rId1 },
            new AmenityRoom { AmenityRoomId = Guid.NewGuid(), AmenityId = amId5, RoomId = rId2 },
            new AmenityRoom { AmenityRoomId = Guid.NewGuid(), AmenityId = amId2, RoomId = rId2 }
        );

      });
    }
  }
}
