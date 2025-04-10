using PayrollWK.ConsoleApp.Domain.Entities;

namespace PayrollWK.ConsoleApp.Application.Interfaces
{
    public interface IINSSCalcService
    {
        decimal ComputeBaseINSS(Employee employee);
        decimal ComputeDeductionINSS(decimal baseInss);
    }
}
