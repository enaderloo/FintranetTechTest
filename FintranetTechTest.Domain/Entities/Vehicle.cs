
using FintranetTechTest.Abstractions.Domain;
using FintranetTechTest.Domain.Enums;
using FintranetTechTest.Domain.ValueObjects;

namespace FintranetTechTest.Domain.Entities
{
    public class Vehicle : AggregateRoot<VehicleId>
    {
        public VehicleType Type { get; set; }

        public Vehicle()
        {
        }

        public Vehicle(int id, VehicleType type)
        {
            Id = id;
            Type = type;
        }
    }
}
