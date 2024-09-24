using Microsoft.EntityFrameworkCore;
using motcyApi.Domain.Entities;

namespace motcyApi.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Motorcycle> Motorcycles { get; set; }
    public DbSet<DeliveryPerson> DeliveryPeople { get; set; }
    public DbSet<Rental> Rentals { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Motorcycle>()
            .HasIndex(m => m.Plate)
            .IsUnique();

        modelBuilder.Entity<DeliveryPerson>()
            .HasIndex(d => d.Cnpj)
            .IsUnique();

        modelBuilder.Entity<DeliveryPerson>()
            .HasIndex(d => d.LicenseNumber)
            .IsUnique();

        modelBuilder.Entity<DeliveryPerson>()
            .Property(d => d.DateOfBirth)
            .HasConversion(
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            );

        modelBuilder.Entity<Rental>()
            .Property(d => d.StartDate)
            .HasConversion(
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            );

        modelBuilder.Entity<Rental>()
            .Property(d => d.EndDate)
            .HasConversion(
                v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : (DateTime?)null,
                v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : (DateTime?)null
            );

        modelBuilder.Entity<Rental>()
            .Property(d => d.ExpectedEndDate)
            .HasConversion(
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            );

        // Relationships
        modelBuilder.Entity<Rental>()
            .HasOne(r => r.Motorcycle)
            .WithMany(m => m.Rentals)
            .HasForeignKey(r => r.MotorcycleId)
            .HasPrincipalKey(m => m.Id);

        modelBuilder.Entity<Rental>()
            .HasOne(r => r.DeliveryPerson)
            .WithMany(d => d.Rentals)
            .HasForeignKey(r => r.DeliveryPersonId)
            .HasPrincipalKey(d => d.Id);

        base.OnModelCreating(modelBuilder);
    }
}
