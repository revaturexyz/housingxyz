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
        entity.HasOne(t => t.Car).WithMany(c => c.Tenant).HasForeignKey(t => t.CarId);
        entity.HasOne(t => t.Batch).WithMany(b => b.Tenant).HasForeignKey(t => t.BatchId);

        entity.HasData
        (
          new Tenant()
          {
            AddressId = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67e"),
            BatchId = 1,
            CarId = 2,
            Email = "nick@revature.com",
            FirstName = "Nick",
            Gender = "Male",
            Id = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67f"),
            LastName = "Escalnoa",
            RoomId = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67g"),
            TrainingCenter = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a670")
          },
          new Tenant()
          {
            AddressId = Guid.Parse("0a4d6c6e-9650-44c9-8c6b-5aebd3f9a67e"),
            BatchId = 2,
            CarId = 1,
            Email = "sue@revature.com",
            FirstName = "Sue",
            Gender = "Female",
            Id = Guid.Parse("0a4d6c6e-9650-44c9-8c6b-5aebd3f9a67f"),
            LastName = "Lemons",
            RoomId = Guid.Parse("0a4d6c6e-9650-44c9-8c6b-5aebd3f9a67g"),
            TrainingCenter = Guid.Parse("0a4d6c6e-9650-44c9-8c6b-5aebd3f9a670")
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
       entity.Property(b => b.BatchLanguage).IsRequired().HasMaxLength(100);
       entity.Property(b => b.StartDate).IsRequired();
       entity.Property(b => b.EndDate).IsRequired();
       entity.Property(b => b.TrainingCenter).IsRequired();

       entity.HasData
       (
         new Batch()
         {
           BatchLanguage = "C#",
           EndDate = new DateTime(2019,12,31),
           Id = 1,
           StartDate = new DateTime(2019, 09, 31),
           TrainingCenter = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a670")
         },
         new Batch()
         {
           BatchLanguage = "Java",
           EndDate = new DateTime(2019, 11, 31),
           Id = 2,
           StartDate = new DateTime(2019, 08, 31),
           TrainingCenter = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a670")
         }
       );

     });
    }
  }
}
