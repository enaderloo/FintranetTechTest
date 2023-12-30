using FintranetTechTest.Abstractions.Exceptions;

namespace FintranetTechTest.Application.Exceptions
{
    public class VehicleNotFound : VehicleException
    {
        public int Id { get; }

        public VehicleNotFound(int id) : base($"Vehicle with ID '{id}' was not found.")
            => Id = id;
    }
}
