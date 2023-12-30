
namespace FintranetTechTest.Abstractions.Domain
{
    public interface IHoliday
    {
        bool IsPublicHoliday(int year, int month, int day);
        bool IsDayBeforePublicHoliday(int year, int month, int day);
    }
}
