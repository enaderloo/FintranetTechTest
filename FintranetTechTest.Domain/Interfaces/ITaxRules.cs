using FintranetTechTest.Domain.Models;

namespace FintranetTechTest.Domain.Providers
{
    public interface ITaxRules
    {
        CongestionTaxRules GetTaxRules();
    }
}
