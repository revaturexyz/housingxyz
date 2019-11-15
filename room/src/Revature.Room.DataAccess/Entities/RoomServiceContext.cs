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
        entity.Property(r => r.RoomID).ValueGeneratedOnAdd();
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
        
      });
    }
  }
}
