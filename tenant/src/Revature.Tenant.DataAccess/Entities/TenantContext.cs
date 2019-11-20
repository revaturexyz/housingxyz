using Microsoft.EntityFrameworkCore;

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
        entity.Property(t => t.BatchId).IsRequired();
        entity.HasOne(t => t.Car).WithMany(c => c.Tenant).HasForeignKey(t => t.CarId);
        entity.HasOne(t => t.Batch).WithMany(b => b.Tenant).HasForeignKey(t => t.BatchId);

        entity.HasData
        (
          new Tenant()
          {
            Email = "nick@revature.com",
            Gender = "Male",
            FirstName = "Nick",
            LastName = "Escals",
            BatchId = 1,
            CarId = 2
          },
          new Tenant()
          {
            Email = "sue@revature.com",
            Gender = "Female",
            FirstName = "Sue",
            LastName = "Lemons",
            BatchId = 2,
            CarId = 1
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
              LicensePlate = "ABC123",
              Make = "Ford",
              Model = "F150",
              Color = "White",
              Year = "1996",
              State = "Louisiana"
            },
            new Car()
            {
              LicensePlate = "456DEF",
              Make = "Honda",
              Model = "VTX 1300",
              Color = "Orange",
              Year = "2006",
              State = "Texas"
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

       entity.HasData
        (
            new Batch()
            {
              BatchLanguage = "C#",
              StartDate = new System.DateTime(2018, 02, 11),
              EndDate = new System.DateTime(2019, 06, 07)
            },
            new Batch()
            {
              BatchLanguage = "Java",
              StartDate = new System.DateTime(2019, 01, 31),
              EndDate = new System.DateTime(2019, 08, 22)
            }
        );

     });
    }
  }
}
