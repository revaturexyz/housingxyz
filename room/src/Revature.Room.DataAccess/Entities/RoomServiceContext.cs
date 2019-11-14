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
      modelBuilder.Entity<Room>(entity =>
      {
        entity.Property(r => r.RoomID)
        .UseIdentityColumn();

        entity.Property(r => r.RoomType)
        .IsRequired();

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
         new Entities.Room()
         {
           RoomID = 1,
           RoomNumber = "2428B",
           RoomType = Lib.RoomType.Apartment,
           NumberOfBeds = 4,
           Gender = Lib.Gender.Female,
           ComplexID = 1,
           LeaseEnd = DateTime.Today.AddMonths(2),
           LeaseStart = DateTime.Now.AddHours(3)
         },
        new Entities.Room()
        {
          RoomID = 2,
          RoomNumber = "221B",
          RoomType = Lib.RoomType.Apartment,
          NumberOfBeds = 4,
          Gender = Lib.Gender.Male,
          ComplexID = 1,
          LeaseEnd = DateTime.Today.AddMonths(2),
          LeaseStart = DateTime.Now.AddHours(3)
        },
        new Lib.Room()
        {
          RoomID = 3,
          ComplexID = 2,
          Gender = Lib.Gender.Female,
          LeaseEnd = new DateTime(2020, 1, 14, 0, 0, 0, 0, DateTimeKind.Local),
          LeaseStart = new DateTime(2019, 11, 14, 16, 59, 33, 250, DateTimeKind.Local).AddTicks(756),
          NumberOfBeds = 4,
          RoomNumber = "2428B",
          RoomType = Lib.RoomType.Apartment
        },
        new Lib.Room()
        {
          RoomID = 4,
          ComplexID = 2,
          Gender = Lib.Gender.Male,
          LeaseEnd = new DateTime(2020, 1, 14, 0, 0, 0, 0, DateTimeKind.Local),
          LeaseStart = new DateTime(2019, 11, 14, 16, 59, 33, 250, DateTimeKind.Local).AddTicks(1503),
          NumberOfBeds = 4,
          RoomNumber = "221B",
          RoomType = Lib.RoomType.Apartment
        }
        );
      });
    }
  }
}
