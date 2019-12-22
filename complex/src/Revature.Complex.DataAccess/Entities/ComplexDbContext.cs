using Microsoft.EntityFrameworkCore;
using System;

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

    private Guid cId1 = Guid.Parse("b5e050aa-6bfc-46ad-9a69-90b1f99ed606");
    //cId1 equals to room service seed data: complex Id
    private Guid cId2 = Guid.Parse("68b7eadd-30ce-49a7-9b8c-bae1d47f46a6");
    private Guid cId3 = Guid.Parse("78b7eadd-30ce-49a7-9b8c-bae1d47f46a6");
    private Guid cId4 = Guid.Parse("88b7eadd-30ce-49a7-9b8c-bae1d47f46a6");
    private Guid rId1 = Guid.Parse("249e5358-169a-4bc6-aa0f-c054952456fd");
    private Guid rId2 = Guid.Parse("fa1d6c6e-9650-44c9-8c6b-5aebd3f9a671");
    //rId1, rId2 equals to room service: room id#1 & room id#2
    private Guid amId1 = Guid.Parse("b8b7eadd-30ce-49a7-9b8c-bae1d47f46a6");
    private Guid amId2 = Guid.Parse("c8b7eadd-30ce-49a7-9b8c-bae1d47f46a6");
    private Guid amId3 = Guid.Parse("d8b7eadd-30ce-49a7-9b8c-bae1d47f46a6");
    private Guid amId4 = Guid.Parse("e8b7eadd-30ce-49a7-9b8c-bae1d47f46a6");
    private Guid amId5 = Guid.Parse("f8b7eadd-30ce-49a7-9b8c-bae1d47f46a6");

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
          .HasMaxLength(50);

        entity.Property(e => e.ContactNumber)
          .HasMaxLength(20);

        entity.HasData(
          new Complex
          {
            ComplexId = cId1,
            AddressId = Guid.Parse("50b7eadd-30ce-49a7-9b8c-bae1d47f46a6"),
            ProviderId = Guid.Parse("51b7eadd-30ce-49a7-9b8c-bae1d47f46a6"),
            ComplexName = "Liv+",
            ContactNumber = "8177517911"
          },
          new Complex
          {
            ComplexId = cId2,
            AddressId = Guid.Parse("52b7eadd-30ce-49a7-9b8c-bae1d47f46a6"),
            ProviderId = Guid.Parse("53b7eadd-30ce-49a7-9b8c-bae1d47f46a6"),
            ComplexName = "SampleComplex",
            ContactNumber = "4445550506"
          },
          new Complex
          {
            ComplexId = cId3,
            AddressId = Guid.Parse("54b7eadd-30ce-49a7-9b8c-bae1d47f46a6"),
            ProviderId = Guid.Parse("55b7eadd-30ce-49a7-9b8c-bae1d47f46a6"),
            ComplexName = "Complex",
            ContactNumber = "7771112222"
          },
          new Complex
          {
            ComplexId = cId4,
            AddressId = Guid.Parse("56b7eadd-30ce-49a7-9b8c-bae1d47f46a6"),
            ProviderId = Guid.Parse("57b7eadd-30ce-49a7-9b8c-bae1d47f46a6"),
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
          .IsRequired()
          .HasMaxLength(50);

        entity.Property(e => e.Description)
          .HasMaxLength(100);

        entity.HasData
        (
          new Amenity { AmenityId = amId1, AmenityType = "fridge", Description = "to keep food fresh" },
          new Amenity { AmenityId = amId2, AmenityType = "microwave", Description = "" },
          new Amenity { AmenityId = amId3, AmenityType = "pool", Description = "swimming" },
          new Amenity { AmenityId = amId4, AmenityType = "kitchen", Description = "cook" },
          new Amenity { AmenityId = amId5, AmenityType = "gym", Description = "work out" }
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
          .WithMany(d => d.AmenityComplex)
          .HasForeignKey(p => p.ComplexId)
          .IsRequired()
          .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasData
        (
          new AmenityComplex { AmenityComplexId = Guid.Parse("58b7eadd-30ce-49a7-9b8c-bae1d47f46a6"), AmenityId = amId1, ComplexId = cId1 },
          new AmenityComplex { AmenityComplexId = Guid.Parse("59b7eadd-30ce-49a7-9b8c-bae1d47f46a6"), AmenityId = amId2, ComplexId = cId1 },
          new AmenityComplex { AmenityComplexId = Guid.Parse("5ab7eadd-30ce-49a7-9b8c-bae1d47f46a6"), AmenityId = amId2, ComplexId = cId2 },
          new AmenityComplex { AmenityComplexId = Guid.Parse("5bb7eadd-30ce-49a7-9b8c-bae1d47f46a6"), AmenityId = amId4, ComplexId = cId2 }
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
          new AmenityRoom { AmenityRoomId = Guid.Parse("5cb7eadd-30ce-49a7-9b8c-bae1d47f46a6"), AmenityId = amId1, RoomId = rId1 },
          new AmenityRoom { AmenityRoomId = Guid.Parse("5db7eadd-30ce-49a7-9b8c-bae1d47f46a6"), AmenityId = amId4, RoomId = rId1 },
          new AmenityRoom { AmenityRoomId = Guid.Parse("5eb7eadd-30ce-49a7-9b8c-bae1d47f46a6"), AmenityId = amId5, RoomId = rId2 },
          new AmenityRoom { AmenityRoomId = Guid.Parse("5fb7eadd-30ce-49a7-9b8c-bae1d47f46a6"), AmenityId = amId2, RoomId = rId2 }
        );
      });
    }
  }
}
