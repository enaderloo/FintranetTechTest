

using FintranetTechTest.Domain.Exceptions;

namespace FintranetTechTest.Domain.ValueObjects
{
    public record CongestionTaxId
    {
        public Guid Value { get; }

        public CongestionTaxId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new VehicleIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(CongestionTaxId id)
          => id.Value;

        public static implicit operator CongestionTaxId(Guid id)
            => new(id);
    }
}
