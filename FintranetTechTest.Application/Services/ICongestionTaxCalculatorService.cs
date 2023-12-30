
using FintranetTechTest.Domain.Enums;
using FintranetTechTest.Domain.Models;

namespace FintranetTechTest.Application.Services
{
    public interface ICongestionTaxCalculatorService
    {
        bool IsWeekend(DateTime date, List<DayOfWeek> dayOfWeeks);
        decimal CalculateCongestionTax(CongestionTaxCalculationInput input);
        bool IsExemptVehicleType(VehicleType vehicleType, List<VehicleType> exemptedVehicleTypes);
        bool IsApplicable(DateTime currentTime, VehicleType vehicleType, CongestionTaxRules rules);
    }
}
