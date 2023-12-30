
namespace FintranetTechTest.Abstractions.Exceptions
{
    public abstract class VehicleException : Exception
    {
        protected VehicleException(string message) : base(message)
        { }
    }
}
