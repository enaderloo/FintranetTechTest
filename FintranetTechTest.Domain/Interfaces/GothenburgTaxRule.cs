using FintranetTechTest.Domain.Enums;
using FintranetTechTest.Domain.Models;
using FintranetTechTest.Domain.Providers;

namespace FintranetTechTest.Infrastructure.Services
{
    public class GothenburgTaxRule : ITaxRules
    {
        public CongestionTaxRules GetTaxRules()
        {
            var rules = new CongestionTaxRules
            {
                //    HourlyTaxAmounts = new Dictionary<TimeOnly, int>
                //{
                //    { new TimeOnly(6, 0, 0), 8 },
                //    { new TimeOnly(6, 30, 0), 13 },
                //    { new TimeOnly(7, 0, 0), 18 },
                //    { new TimeOnly(8, 0, 0), 13 },
                //    { new TimeOnly(8, 30, 0), 8 },
                //    { new TimeOnly(15, 0, 0), 13 },
                //    { new TimeOnly(15, 30, 0), 18 },
                //    { new TimeOnly(17, 0, 0), 13 },
                //    { new TimeOnly(18, 0, 0), 8 },
                //    { new TimeOnly(18, 30, 0), 0 },
                //    { new TimeOnly(0, 0, 0), 0 },
                //},
                MaxTaxPerDay = 60,
                ExemptedDays = new List<DayOfWeek> { DayOfWeek.Saturday, DayOfWeek.Sunday },
                ExemptedDates = new List<PublicHoliday>
                {
                    new PublicHoliday(1, 1),
                    new PublicHoliday(3, 28, 29),
                    new PublicHoliday(4, 1, 30),
                    new PublicHoliday(5, 1, 8, 9),
                    new PublicHoliday(6, 5, 6, 21),
                    new PublicHoliday(7),
                    new PublicHoliday(11, 1),
                    new PublicHoliday(12, 24, 25, 26, 31)
                },
                SingleChargeRuleMinutes = 60,
                ExemptedVehicleTypes = new List<VehicleType>
                {
                    VehicleType.Emergency,
                    VehicleType.Bus,
                    VehicleType.Diplomat,
                    VehicleType.Motorcycle,
                    VehicleType.Military,
                    VehicleType.Foreign
                }
            };
            return rules;
        }
    }
}
