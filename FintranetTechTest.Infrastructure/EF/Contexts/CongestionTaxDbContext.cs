using FintranetTechTest.Domain.Entities;
using FintranetTechTest.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace FintranetTechTest.Infrastructure.EF.Contexts
{
    public class CongestionTaxDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }

        public CongestionTaxDbContext(DbContextOptions<CongestionTaxDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "InMemoryCongestionTaxDatabase");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle { Type = VehicleType.Emergency },
                new Vehicle { Type = VehicleType.Bus },
                new Vehicle { Type = VehicleType.Diplomat },
                new Vehicle { Type = VehicleType.Motorcycle },
                new Vehicle { Type = VehicleType.Military },
                new Vehicle { Type = VehicleType.Foreign },
                new Vehicle { Type = VehicleType.Car },
                new Vehicle { Type = VehicleType.Truk }
            );
        }
    }
}
