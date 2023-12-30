using FintranetTechTest.Abstractions.Domain;
using FintranetTechTest.Domain.Models;

namespace FintranetTechTest.Application.Services
{
    public class HolidayChecker : IHoliday
    {
        private readonly List<PublicHoliday> publicHolidays = new()
        {
        new PublicHoliday(1, 1),
        new PublicHoliday(3, 28, 29),
        new PublicHoliday(4, 1, 30),
        new PublicHoliday(5, 1, 8, 9),
        new PublicHoliday(6, 5, 6, 21),
        new PublicHoliday(7),
        new PublicHoliday(11, 1),
        new PublicHoliday(12, 24, 25, 26, 31)
    };

        public bool IsPublicHoliday(int year, int month, int day)
        {
            var holidaysForMonth = publicHolidays.Find(ph => ph.Month == month);
            return holidaysForMonth?.Days.Contains(day) ?? false;
        }

        public bool IsDayBeforePublicHoliday(int year, int month, int day)
        {
            DateTime previousDay = new DateTime(year, month, day).AddDays(-1);
            return IsPublicHoliday(previousDay.Year, previousDay.Month, previousDay.Day);
        }
    }
}
