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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Gender>(entity =>
      {
        entity.HasData(
          new Gender() { Type = "Male"},
          new Gender() { Type = "Female"},
          new Gender() { Type = "NonBinary"}
          );
      });

      modelBuilder.Entity<RoomType>(
        entity =>
        {
          entity.HasData(
            new RoomType() { Type = "Apartment" },
            new RoomType() { Type = "Dormitory" },
            new RoomType() { Type = "TownHouse" }
            );
        }
        );

      modelBuilder.Entity<Room>(entity =>
      {
        entity.Property(r => r.RoomID);
        entity.Property(r => r.RoomNumber)
        .IsRequired();

        entity.Property(r => r.NumberOfBeds)
        .IsRequired();

        entity.Property(r => r.LeaseEnd)
        .IsRequired();

        entity.Property(r => r.LeaseStart)
        .IsRequired();

        entity.Property(r => r.ComplexID)
        .IsRequired();
        entity.HasData(
          new
          {
            RoomTypeType = "Apartment",
            GenderType = "Female",
            LeaseEnd = DateTime.Today.AddMonths(3),
            LeaseStart = DateTime.Now,
            RoomID = Guid.Parse("249e5358-169a-4bc6-aa0f-c054952456fd"),
            ComplexID = Guid.Parse("b5e050aa-6bfc-46ad-9a69-90b1f99ed606"),
            NumberOfBeds = 4,
            RoomNumber = "2428B"
          }
          ); 
      });
    }
  }
}
