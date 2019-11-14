using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

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
        }
        ); 
      });
    }
  }
}
