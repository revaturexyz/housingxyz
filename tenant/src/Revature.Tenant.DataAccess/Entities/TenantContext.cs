using Microsoft.EntityFrameworkCore;
using System;

namespace Revature.Tenant.DataAccess.Entities
{
  public class TenantContext : DbContext
  {
    public TenantContext() { }

    public TenantContext(DbContextOptions<TenantContext> options) : base(options) { }

    public virtual DbSet<Tenant> Tenant { get; set; }
    public virtual DbSet<Car> Car { get; set; }
    public virtual DbSet<Batch> Batch { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Tenant>(entity =>
      {
        entity.HasKey(t => t.Id);
        entity.Property(t => t.Email).IsRequired();
        entity.Property(t => t.Gender).IsRequired();
        entity.Property(t => t.FirstName).IsRequired().HasMaxLength(100);
        entity.Property(t => t.LastName).IsRequired().HasMaxLength(100);
        entity.Property(t => t.TrainingCenter).IsRequired();
        entity.HasOne(t => t.Car).WithOne(c => c.Tenant).HasForeignKey<Tenant>(t => t.CarId);
        entity.HasOne(t => t.Batch).WithMany(b => b.Tenant).HasForeignKey(t => t.BatchId);

        entity.HasData
        (
          new Tenant()
          {
            AddressId = Guid.Parse("1a4d6c6e-9650-44c9-8c6b-5aebd3f9a67e"),
            BatchId = 1,
            CarId = 2,
            Email = "nick@revature.com",
            FirstName = "Nick",
            Gender = "Male",
            Id = Guid.Parse("f14d6c6e-9650-44c9-8c6b-5aebd3f9a67f"),
            LastName = "Escalnoa",
            RoomId = Guid.Parse("fa1d6c6e-9650-44c9-8c6b-5aebd3f9a671"),
            TrainingCenter = Guid.Parse("fa416c6e-9650-44c9-8c6b-5aebd3f9a670")
          },
          new Tenant()
          {
            AddressId = Guid.Parse("0a4d616e-9650-44c9-8c6b-5aebd3f9a67e"),
            BatchId = 2,
            CarId = 1,
            Email = "sue@revature.com",
            FirstName = "Sue",
            Gender = "Female",
            Id = Guid.Parse("0a4d6c1e-9650-44c9-8c6b-5aebd3f9a67f"),
            LastName = "Lemons",
            RoomId = Guid.Parse("0a4d6c61-9650-44c9-8c6b-5aebd3f9a676"),
            TrainingCenter = Guid.Parse("fa416c6e-9650-44c9-8c6b-5aebd3f9a670")
          }
        );

      });

      builder.Entity<Car>(entity =>
      {
        entity.HasKey(c => c.Id);
        entity.Property(c => c.Id).UseIdentityColumn();
        entity.Property(c => c.LicensePlate).IsRequired().HasMaxLength(100);
        entity.Property(c => c.Make).IsRequired().HasMaxLength(100);
        entity.Property(c => c.Model).IsRequired().HasMaxLength(100);
        entity.Property(c => c.Color).IsRequired().HasMaxLength(100);
        entity.Property(c => c.Year).IsRequired();
        entity.Property(c => c.State).IsRequired();

        entity.HasData
        (
          new Car()
          {
            Color = "White",
            Id = 1,
            LicensePlate = "ABC123",
            Make = "Ford",
            Model = "F150",
            State = "LA",
            Year = "1996"
          },
          new Car()
          {
            Color = "Orange",
            Id = 2,
            LicensePlate = "DEF456",
            Make = "Honda",
            Model = "VTX1300",
            State = "TX",
            Year = "2006"
          }
        );

      });

      builder.Entity<Batch>(entity =>
     {
       entity.HasKey(b => b.Id);
       entity.Property(b => b.Id).UseIdentityColumn();
       entity.Property(b => b.BatchCurriculum).IsRequired().HasMaxLength(100);
       entity.Property(b => b.StartDate).IsRequired();
       entity.Property(b => b.EndDate).IsRequired();
       entity.Property(b => b.TrainingCenter).IsRequired();

       entity.HasData
       (
         new Batch()
         {
           BatchCurriculum = "C#",
           EndDate = new DateTime(2019,12,30),
           Id = 1,
           StartDate = new DateTime(2019, 09, 30),
           TrainingCenter = Guid.Parse("fa416c6e-9650-44c9-8c6b-5aebd3f9a670")
         },
         new Batch()
         {
           BatchCurriculum = "Java",
           EndDate = new DateTime(2019, 11, 30),
           Id = 2,
           StartDate = new DateTime(2019, 08, 30),
           TrainingCenter = Guid.Parse("fa416c6e-9650-44c9-8c6b-5aebd3f9a670")
         }
       );
     });
    }
  }
}
