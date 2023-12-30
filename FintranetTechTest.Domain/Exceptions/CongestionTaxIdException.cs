
using FintranetTechTest.Abstractions.Exceptions;

namespace FintranetTechTest.Domain.Exceptions
{
    public class CongestionTaxIdException : CongestionTaxException
    {
        public CongestionTaxIdException() : base("Congestion Tax ID cannot be empty.")
        {
        }
    }
}
