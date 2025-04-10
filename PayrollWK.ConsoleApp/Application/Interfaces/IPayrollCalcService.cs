using PayrollWK.ConsoleApp.Domain.Entities;

namespace PayrollWK.ConsoleApp.Application.Interfaces
{
    public interface IPayrollCalcService
    {
        PayrollResult Calculate(Employee employee);
    }
}
