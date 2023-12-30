using FintranetTechTest.Domain.Entities;
using FintranetTechTest.Domain.ValueObjects;

namespace FintranetTechTest.Domain.Repositories
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetAsync(VehicleId id);
        Task AddAsync(Vehicle vehicle);
        Task UpdateAsync(Vehicle vehicle);
        Task DeleteAsync(Vehicle vehicle);
    }
}
