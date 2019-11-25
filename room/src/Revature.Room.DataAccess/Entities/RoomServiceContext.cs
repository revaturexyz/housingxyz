using Microsoft.EntityFrameworkCore;
using System;

namespace Revature.Room.DataAccess.Entities
{
  public class RoomServiceContext : DbContext
  {
    public RoomServiceContext()
    {
    }

    public RoomServiceContext(DbContextOptions<RoomServiceContext> options)
        : base(options)
    {
    }

    public DbSet<Room> Room { get; set; }
    public DbSet<Gender> Gender { get; set; }

    public DbSet<RoomType> RoomType { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Gender>(entity =>
      {
        entity.HasKey(g => g.GenderId);
        entity.Property(g => g.GenderId)
          .UseIdentityColumn()
          .IsRequired();

        entity.Property(g => g.Type)
          .IsRequired();

        entity.HasData(
          new Gender() { GenderId = 1, Type = "Male" },
          new Gender() { GenderId = 2, Type = "Female" }
          );
      });

      modelBuilder.Entity<RoomType>(

        entity =>
        {
          entity.HasKey(r => r.RoomTypeId);
          entity.Property(r => r.RoomTypeId)
            .UseIdentityColumn()
            .IsRequired();
          entity.Property(r => r.Type)
            .IsRequired();
          entity.HasData(
            new RoomType() { RoomTypeId = 1, Type = "Apartment" },
            new RoomType() { RoomTypeId = 2, Type = "Dormitory" },
            new RoomType() { RoomTypeId = 3, Type = "TownHouse" }
            );
        }
        );

      modelBuilder.Entity<Room>(entity =>
      {
        entity.HasKey(r => r.RoomId);
        entity.Property(r => r.RoomNumber)
          .IsRequired();

        entity.Property(r => r.NumberOfBeds)
          .IsRequired();

        entity.Property(r => r.LeaseEnd)
          .IsRequired();

        entity.Property(r => r.LeaseStart)
          .IsRequired();

        entity.Property(r => r.ComplexId)
          .IsRequired();

        entity.Property(r => r.NumberOfOccupants)
          .IsRequired();

        entity.HasData(
          new
          {
            RoomTypeId = 1,
            GenderId = 1,
            LeaseEnd = DateTime.Today.AddMonths(3),
            LeaseStart = DateTime.Now,
            RoomId = Guid.Parse("249e5358-169a-4bc6-aa0f-c054952456fd"),
            ComplexId = Guid.Parse("b5e050aa-6bfc-46ad-9a69-90b1f99ed606"),
            NumberOfBeds = 4,
            RoomNumber = "2428B",
            NumberOfOccupants = 2
          },
          new
          {
            RoomTypeId = 1,
            GenderId = 1,
            LeaseEnd = DateTime.Today.AddMonths(3),
            LeaseStart = DateTime.Now,
            RoomId = Guid.Parse("fa1d6c6e-9650-44c9-8c6b-5aebd3f9a671"),
            ComplexId = Guid.Parse("b5e050aa-6bfc-46ad-9a69-90b1f99ed606"),
            NumberOfBeds = 4,
            RoomNumber = "2428B",
            NumberOfOccupants = 1
          },
          new
          {
            RoomTypeId = 1,
            GenderId = 2,
            LeaseEnd = DateTime.Today.AddMonths(3),
            LeaseStart = DateTime.Now,
            RoomId = Guid.Parse("0a4d6c61-9650-44c9-8c6b-5aebd3f9a676"),
            ComplexId = Guid.Parse("b5e050aa-6bfc-46ad-9a69-90b1f99ed606"),
            NumberOfBeds = 4,
            RoomNumber = "2428B",
            NumberOfOccupants = 1
          }
          );
      });
    }
  }
}
