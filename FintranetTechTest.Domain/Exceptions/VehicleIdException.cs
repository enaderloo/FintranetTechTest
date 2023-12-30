
using FintranetTechTest.Abstractions.Exceptions;

namespace FintranetTechTest.Domain.Exceptions
{
    public class VehicleIdException : VehicleException
    {
        public VehicleIdException() : base("Vehicle ID cannot be empty.")
        {
        }
    }
}
