
namespace FintranetTechTest.Abstractions.Exceptions
{
    public abstract class CongestionTaxException : Exception
    {
        protected CongestionTaxException(string message) : base(message)
        { }
    }
}
