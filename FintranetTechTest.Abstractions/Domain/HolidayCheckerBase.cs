
namespace FintranetTechTest.Abstractions.Domain
{
    public abstract class HolidayCheckerBase
    {
        protected readonly IHoliday Holiday;

        protected HolidayCheckerBase(IHoliday holiday)
        {
            Holiday = holiday ?? throw new ArgumentNullException(nameof(holiday));
        }

        public abstract bool IsPublicHoliday(int year, int month, int day);
        public abstract bool IsDayBeforePublicHoliday(int year, int month, int day);
    }
}
