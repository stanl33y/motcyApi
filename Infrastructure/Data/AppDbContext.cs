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
