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

      });
    }
  }
}
