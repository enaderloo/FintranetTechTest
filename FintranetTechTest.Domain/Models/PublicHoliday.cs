
namespace FintranetTechTest.Domain.Models
{
    public class PublicHoliday
    {
        public int Month { get; }
        public List<int> Days { get; }

        public PublicHoliday(int month, params int[] days)
        {
            Month = month;
            Days = new List<int>(days);
        }
    }
}
