using FintranetTechTest.Domain.Entities;

namespace FintranetTechTest.Domain.Models
{
    public record CongestionTaxCalculationInput
    {
        public required Vehicle Vehicle { get; set; }
        public required List<DateTime> Dates { get; set; }
    }
}
