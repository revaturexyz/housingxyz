using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Xyz.Provider.DataAccess.Entities
{
  /// <summary>
  /// DbContext used to construct/alter SQL DB with migrations
  /// </summary>
  /// <remarks>Configured in Startup so migrations after initial create won't work</remarks>
  public partial class RevatureHousingDbContext : DbContext
  {
    /// <summary>
    /// Constructs a RevatureHousingDbContext without options, to be configured externally
    /// </summary>
    public RevatureHousingDbContext()
    {
    }

    /// <summary>
    /// Constructs a context from the parent constructor with settings configured in options
    /// </summary>
    /// <param name="options">DbContextOptions with settings for this DbContext</param>
    public RevatureHousingDbContext(DbContextOptions<RevatureHousingDbContext> options)
      : base(options)
    {
    }

    /// <summary>
    /// DbSet representing Address table
    /// </summary>
    public virtual DbSet<Address> Address { get; set; }

    /// <summary>
    /// DbSet representing Amenity table
    /// </summary>
    public virtual DbSet<Amenity> Amenity { get; set; }

    /// <summary>
    /// DbSet representing Complex table
    /// </summary>
    public virtual DbSet<Complex> Complex { get; set; }

    /// <summary>
    /// DbSet representing Gender table
    /// </summary>
    public virtual DbSet<Gender> Gender { get; set; }

    /// <summary>
    /// DbSet representing Notification table
    /// </summary>
    public virtual DbSet<Notification> Notification { get; set; }

    /// <summary>
    /// DbSet representing Provider table
    /// </summary>
    public virtual DbSet<Provider> Provider { get; set; }

    /// <summary>
    /// DbSet representing Room table
    /// </summary>
    public virtual DbSet<Room> Room { get; set; }

    /// <summary>
    /// DbSet representing RoomAmenity join table
    /// </summary>
    public virtual DbSet<RoomAmenity> RoomAmenity { get; set; }

    /// <summary>
    /// DbSet representing TrainingCenter table
    /// </summary>
    public virtual DbSet<TrainingCenter> TrainingCenter { get; set; }

    /// <summary>
    /// DbSet representing RoomType table
    /// </summary>
    public virtual DbSet<RoomType> RoomType { get; set; }

    /// <summary>
    /// Determines structure of DB represented by this DbContext,
    /// configured for code-first migrations
    /// </summary>
    /// <param name="modelBuilder">EF object for constructing DbContext model</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Address>(entity =>
      {
        entity.Property(e => e.City)
          .IsRequired()
          .HasMaxLength(60);

        entity.Property(e => e.State)
          .IsRequired()
          .HasMaxLength(60);

        entity.Property(e => e.StreetAddress)
          .IsRequired()
          .HasMaxLength(256);

        entity.Property(e => e.Zip)
          .IsRequired()
          .HasMaxLength(5);

        entity.HasData(
          new Address { AddressId = 1, StreetAddress = "1001 S. Center St.", City = "Arlington", State = "TX", Zip = "76010" },
          new Address { AddressId = 2, StreetAddress = "701 S. Nedderman Drive", City = "Arlington", State = "TX", Zip = "76010" },
          new Address { AddressId = 3, StreetAddress = "805 S Center St", City = "Arlington", State = "TX", Zip = "72712" },
          new Address { AddressId = 4, StreetAddress = "1001 UTA Blvd", City = "Arlington", State = "TX", Zip = "72712" }
        );
      });

      modelBuilder.Entity<Amenity>(entity =>
      {
        entity.Property(e => e.AmenityType)
          .IsRequired();

        entity.HasData(
           new Amenity { AmenityId = 1, AmenityType = "Washer/Dryer" },
           new Amenity { AmenityId = 2, AmenityType = "Refrigerator" },
           new Amenity { AmenityId = 3, AmenityType = "Full Kitchen" },
           new Amenity { AmenityId = 4, AmenityType = "Gym" },
           new Amenity { AmenityId = 5, AmenityType = "Pool" },
           new Amenity { AmenityId = 6, AmenityType = "Smart TV" },
           new Amenity { AmenityId = 7, AmenityType = "Personal Bathroom" },
           new Amenity { AmenityId = 8, AmenityType = "Shuttle Service" },
           new Amenity { AmenityId = 9, AmenityType = "Clubhouse" },
           new Amenity { AmenityId = 10, AmenityType = "Microwave" }
        );
      });

      modelBuilder.Entity<Complex>(entity =>
      {
        entity.Property(e => e.ComplexName)
          .IsRequired()
          .HasMaxLength(100);

        entity.Property(e => e.ContactNumber)
          .HasMaxLength(20);

        entity.HasOne(d => d.Address)
          .WithMany(p => p.Complex)
          .HasForeignKey(d => d.AddressId)
          .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.Provider)
          .WithMany(p => p.Complex)
          .HasForeignKey(d => d.ProviderId)
          .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasData(
          new Complex { ComplexId = 1, AddressId = 1, ProviderId = 1, ComplexName = "Liv+", ContactNumber = "8177517911" },
          new Complex { ComplexId = 2, AddressId = 2, ProviderId = 1, ComplexName = "SampleComplex", ContactNumber = "4445550506" },
          new Complex { ComplexId = 3, AddressId = 3, ProviderId = 2, ComplexName = "Complex", ContactNumber = "7771112222" },
          new Complex { ComplexId = 4, AddressId = 4, ProviderId = 2, ComplexName = "ComplexNearMe", ContactNumber = "3332221111" }
        );
      });

      modelBuilder.Entity<Gender>(entity =>
      {
        entity.Property(e => e.GenderType)
          .IsRequired()
          .HasMaxLength(20);

        entity.HasData(
          new Gender { GenderId = 1, GenderType = "Male" },
          new Gender { GenderId = 2, GenderType = "Female" },
          new Gender { GenderId = 3, GenderType = "Undefined" }
        );
      });

      modelBuilder.Entity<Notification>(entity =>
      {
        entity.Property(e => e.Reason)
          .HasMaxLength(512);

        entity.Property(e => e.Title)
          .IsRequired()
          .HasMaxLength(100);

        entity.HasOne(d => d.Provider)
          .WithMany(p => p.Notification)
          .HasForeignKey(d => d.ProviderId)
          .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.Room)
          .WithMany(p => p.Notification)
          .HasForeignKey(d => d.RoomId)
          .OnDelete(DeleteBehavior.ClientSetNull);
      });

      modelBuilder.Entity<Provider>(entity =>
      {
        entity.Property(e => e.CompanyName)
          .IsRequired()
          .HasMaxLength(100);

        entity.Property(e => e.ContactNumber)
          .HasMaxLength(20);

        entity.Property(e => e.Password)
          .IsRequired()
          .HasMaxLength(40);

        entity.Property(e => e.Username)
          .IsRequired()
          .HasMaxLength(40);

        entity.HasOne(d => d.Address)
          .WithMany(p => p.Provider)
          .HasForeignKey(d => d.AddressId)
          .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(d => d.Center)
          .WithMany(p => p.Provider)
          .HasForeignKey(d => d.CenterId)
          .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasData(
          new Provider { ProviderId = 1, CompanyName = "Liv+", AddressId = 2, CenterId = 1, ContactNumber = "9213215214", Username = "SomeUserName", Password = Mapper.Password },
          new Provider { ProviderId = 2, CompanyName = "Dov+", AddressId = 1, CenterId = 1, ContactNumber = "5001245125", Username = "SomeProviderName", Password = Mapper.Password }
        );
      });

      modelBuilder.Entity<Room>(entity =>
      {
        entity.HasIndex(e => new { e.AddressId, e.RoomNumber })
          .IsUnique();

        entity.HasOne(d => d.Address)
          .WithMany(p => p.Room)
          .HasForeignKey(d => d.AddressId)
          .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.Complex)
          .WithMany(p => p.Room)
          .HasForeignKey(d => d.ComplexId)
          .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.Gender)
          .WithMany(p => p.Room)
          .HasForeignKey(d => d.GenderId)
          .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.Type)
          .WithMany(p => p.Room)
          .HasForeignKey(d => d.TypeId)
          .OnDelete(DeleteBehavior.ClientSetNull);

        var initRooms = new List<Room>();
        var ran = new Random(1234567890);
        for (int x = 1; x <= 10; x++)
        {
          int rId = ran.Next(1, 4);
          int complexId = ran.Next(1, 4);
          int typeId = ran.Next(1, 2);
          int gId = ran.Next(1, 3);
          int numBeds = ran.Next(2, 4);
          int numOcc = ran.Next(0, 2);
          initRooms.Add(new Room
          {
            RoomId = x,
            AddressId = rId,
            ComplexId = complexId,
            StartDate = new DateTime(2015, 09, 23),
            GenderId = gId,
            NumberOfBeds = numBeds,
            NumberOfOccupants = numOcc,
            RoomNumber = "110" + x,
            TypeId = typeId,
            EndDate = new DateTime(2016, 08, 23)
          });
        }

        entity.HasData(initRooms.ToArray());
      });

      modelBuilder.Entity<RoomAmenity>(entity =>
      {
        entity.HasOne(d => d.Amenity)
          .WithMany(p => p.RoomAmenity)
          .HasForeignKey(d => d.AmenityId)
          .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(d => d.Room)
          .WithMany(p => p.RoomAmenity)
          .HasForeignKey(d => d.RoomId)
          .OnDelete(DeleteBehavior.Cascade);

        entity.HasData(
          new RoomAmenity { RoomAmenityId = 1, AmenityId = 1, RoomId = 1 },
          new RoomAmenity { RoomAmenityId = 2, AmenityId = 1, RoomId = 2 },
          new RoomAmenity { RoomAmenityId = 3, AmenityId = 1, RoomId = 3 },
          new RoomAmenity { RoomAmenityId = 4, AmenityId = 1, RoomId = 4 },
          new RoomAmenity { RoomAmenityId = 5, AmenityId = 1, RoomId = 5 },
          new RoomAmenity { RoomAmenityId = 6, AmenityId = 1, RoomId = 6 },
          new RoomAmenity { RoomAmenityId = 7, AmenityId = 2, RoomId = 1 },
          new RoomAmenity { RoomAmenityId = 8, AmenityId = 3, RoomId = 3 },
          new RoomAmenity { RoomAmenityId = 9, AmenityId = 4, RoomId = 2 },
          new RoomAmenity { RoomAmenityId = 10, AmenityId = 5, RoomId = 5 },
          new RoomAmenity { RoomAmenityId = 11, AmenityId = 7, RoomId = 6 },
          new RoomAmenity { RoomAmenityId = 12, AmenityId = 8, RoomId = 2 },
          new RoomAmenity { RoomAmenityId = 13, AmenityId = 6, RoomId = 6 },
          new RoomAmenity { RoomAmenityId = 14, AmenityId = 9, RoomId = 2 },
          new RoomAmenity { RoomAmenityId = 15, AmenityId = 10, RoomId = 4 },
          new RoomAmenity { RoomAmenityId = 16, AmenityId = 10, RoomId = 3 },
          new RoomAmenity { RoomAmenityId = 17, AmenityId = 5, RoomId = 2 }
        );
      });

      modelBuilder.Entity<TrainingCenter>(entity =>
      {
        entity.HasKey(e => e.CenterId);

        entity.Property(e => e.CenterName)
          .IsRequired()
          .HasMaxLength(60);

        entity.Property(e => e.ContactNumber)
          .HasMaxLength(20);

        entity.HasOne(d => d.Address)
          .WithMany(p => p.TrainingCenter)
          .HasForeignKey(d => d.AddressId)
          .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasData(
           new TrainingCenter { CenterId = 1, AddressId = 1, CenterName = "UTA Revature", ContactNumber = "8008128910" }
        );
      });

      modelBuilder.Entity<RoomType>(entity =>
      {
        entity.Property(e => e.TypeId)
          .IsRequired();

        entity.Property(e => e.Type)
          .HasMaxLength(60)
          .IsRequired();

        entity.HasIndex(e => e.Type)
          .IsUnique();

        entity.HasData(
          new RoomType { TypeId = 1, Type = "Apartment" },
          new RoomType { TypeId = 2, Type = "Dormitory" }
        );
      });
    }
  }
}
