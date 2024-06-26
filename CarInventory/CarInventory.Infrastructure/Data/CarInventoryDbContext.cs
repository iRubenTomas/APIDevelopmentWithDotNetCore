﻿
using CarInventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarInventory.Infrastructure.Data
{
    public class CarInventoryDbContext : DbContext
    {
        public CarInventoryDbContext(DbContextOptions<CarInventoryDbContext> options) : base(options) { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarInventoryDbContext).Assembly);

            // Seed data
            modelBuilder.Entity<Car>().HasData(
                new Car
                {
                    Id = Guid.NewGuid(),
                    Brand = "Toyota",
                    Model = "Camry",
                    Year = 2020,
                    VIN = "1HGBH41JXMN109186",
                    Price = 24000.00m,
                    Status = "Available",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Car
                {
                    Id = Guid.NewGuid(),
                    Brand = "Honda",
                    Model = "Accord",
                    Year = 2019,
                    VIN = "2HGEH41JXMN109187",
                    Price = 22000.00m,
                    Status = "Available",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseIdentity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
