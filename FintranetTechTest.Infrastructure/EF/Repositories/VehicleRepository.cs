using FintranetTechTest.Domain.Entities;
using FintranetTechTest.Domain.Repositories;
using FintranetTechTest.Domain.ValueObjects;
using FintranetTechTest.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FintranetTechTest.Infrastructure.EF.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly CongestionTaxDbContext _congestionTaxDbContext;
        public readonly DbSet<Vehicle> _vehicles;

        public VehicleRepository()
        {

        }
        public VehicleRepository(CongestionTaxDbContext congestionTaxDbContext)
        {
            _vehicles = congestionTaxDbContext.Vehicles;
            _congestionTaxDbContext = congestionTaxDbContext ??
                throw new ArgumentNullException(nameof(congestionTaxDbContext));
        }


        public Task AddAsync(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public async Task<Vehicle> GetAsync(VehicleId id)
        {
            return await _congestionTaxDbContext
                .Vehicles
                 .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task UpdateAsync(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }
    }
}
