
using FintranetTechTest.Domain.Exceptions;

namespace FintranetTechTest.Domain.ValueObjects
{
    public record VehicleId
    {
        public int Value { get; }

        public VehicleId(int value)
        {
            if (value == 0 )
            {
                throw new VehicleIdException();
            }

            Value = value;
        }

        public static implicit operator int(VehicleId id)
          => id.Value;

        public static implicit operator VehicleId(int id)
            => new(id);
    }
}
