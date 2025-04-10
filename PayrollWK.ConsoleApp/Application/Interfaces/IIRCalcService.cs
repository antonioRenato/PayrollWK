using PayrollWK.ConsoleApp.Domain.Entities;

namespace PayrollWK.ConsoleApp.Application.Interfaces
{
    public interface IIRCalcService
    {
        decimal AmountPerDependent { get; }
        decimal ComputeDeductionDependents(int quantityDependents);
        decimal ComputeBaseIR(Employee employee, decimal deductionDependents, decimal baseINSS);
        decimal ComputeDeductionIR(decimal baseIR);
    }
}
