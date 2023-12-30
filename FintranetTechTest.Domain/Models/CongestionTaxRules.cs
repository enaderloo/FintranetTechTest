using FintranetTechTest.Domain.Enums;

namespace FintranetTechTest.Domain.Models
{
    public record CongestionTaxRules
    {
        public int MaxTaxPerDay { get; set; }
        public List<DayOfWeek> ExemptedDays { get; set; }
        public List<PublicHoliday> ExemptedDates { get; set; }
        public int SingleChargeRuleMinutes { get; set; }
        public List<VehicleType> ExemptedVehicleTypes { get; set; }


    }
}
