using FintranetTechTest.Abstractions.Domain;
using FintranetTechTest.Application.Exceptions;
using FintranetTechTest.Domain.Enums;
using FintranetTechTest.Domain.Models;
using FintranetTechTest.Domain.Providers;
using FintranetTechTest.Domain.Repositories;

namespace FintranetTechTest.Application.Services
{
    public class CongestionTaxCalculatorService : HolidayCheckerBase, ICongestionTaxCalculatorService
    {
        private readonly ITaxRules _taxRulesProvider;
        private readonly IVehicleRepository _vehicleRepository;

        public CongestionTaxCalculatorService(
            ITaxRules taxRuleProvider,
            IVehicleRepository vehicleRepository,
            IHoliday holiday
            ) : base(holiday)
        {
            _taxRulesProvider = taxRuleProvider;
            _vehicleRepository = vehicleRepository;
        }

        public override bool IsPublicHoliday(int year, int month, int day)
        {
            return Holiday.IsPublicHoliday(year, month, day);
        }

        public override bool IsDayBeforePublicHoliday(int year, int month, int day)
        {
            return Holiday.IsDayBeforePublicHoliday(year, month, day);
        }

        public decimal CalculateCongestionTax(CongestionTaxCalculationInput input)
        {
            //if (_vehicleRepository.GetAsync(input.Vehicle.Id) is null)
            //    throw new VehicleNotFound(input.Vehicle.Id);

            CongestionTaxRules rules = _taxRulesProvider.GetTaxRules();

            DateTime intervalStart = input.Dates[0];
            int totalFee = 0;

            foreach (var item in input.Dates)
            {
                if (IsApplicable(item, input.Vehicle.Type, rules))
                {
                    int nextFee = GetTaxAmountForTime(item.Hour, item.Minute);
                    int tempFee = GetTaxAmountForTime(intervalStart.Hour, intervalStart.Minute);

                    long diffInMillies = item.Millisecond - intervalStart.Millisecond;
                    long minutes = diffInMillies / 1000 / 60;

                    if (minutes <= rules.SingleChargeRuleMinutes)
                    {
                        if (totalFee > 0) totalFee -= tempFee;
                        if (nextFee >= tempFee) tempFee = nextFee;
                        totalFee += tempFee;
                    }
                    else
                    {
                        totalFee += nextFee;
                    }
                }
            }
            if (totalFee > 60) totalFee = 60;
            return totalFee;
        }

        public bool IsApplicable(DateTime currentTime, VehicleType vehicleType, CongestionTaxRules rules)
        {
            if (IsExemptFromCongestionTax(currentTime, vehicleType, rules))
                return false;

            return true;
        }

        private bool IsExemptFromCongestionTax(DateTime currentTime, VehicleType vehicleType, CongestionTaxRules rules)
        {
            return IsWeekend(currentTime, rules.ExemptedDays)
                || IsPublicHoliday(currentTime.Year, currentTime.Month, currentTime.Day)
                || IsDayBeforePublicHoliday(currentTime.Year, currentTime.Month, currentTime.Day)
                || IsExemptVehicleType(vehicleType, rules.ExemptedVehicleTypes);
        }

        public bool IsWeekend(DateTime date, List<DayOfWeek> dayOfWeeks)
        {
            return dayOfWeeks.Contains(date.DayOfWeek);
        }
        public bool IsExemptVehicleType(VehicleType vehicleType, List<VehicleType> exemptedVehicleTypes)
        {
            return exemptedVehicleTypes.Contains(vehicleType);
        }

        private int GetTaxAmountForTime(int hour, int minute)
        {
            int result = 0;

            if ((hour == 6 && minute >= 0 && minute <= 29) ||
                (hour == 8 && minute >= 0 && minute <= 29) ||
                (hour == 18 && minute >= 0 && minute <= 29))
            {
                result = 8;
            }
            else if ((hour == 6 && minute >= 30 && minute <= 59) ||
                     (hour == 15 && minute >= 0 && minute <= 29) ||
                     (hour == 17 && minute >= 0 && minute <= 59))
            {
                result = 13;
            }
            else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59)
            {
                result = 8;
            }
            else if ((hour == 7 && minute >= 0 && minute <= 59) ||
                     (hour == 16 && minute <= 59) ||
                     (hour == 15 && minute >= 30 && minute <= 59))
            {
                result = 18;
            }

            return result;
        }

    }
}
