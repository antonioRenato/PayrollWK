using PayrollWK.ConsoleApp.Application.Interfaces;
using PayrollWK.ConsoleApp.Domain.Entities;
using PayrollWK.ConsoleApp.Domain.Enums;

namespace PayrollWK.ConsoleApp.Application.Services
{
    public class PayrollCalcService : IPayrollCalcService
    {
        private readonly IINSSCalcService _inssCalc;
        private readonly IIRCalcService _irCalc;

        public PayrollCalcService(IINSSCalcService inssCalc, IIRCalcService irCalc)
        {
            _inssCalc = inssCalc;
            _irCalc = irCalc;
        }

        public PayrollResult Calculate(Employee employee)
        {
            var totalEarnings = employee.Headings
                .Where(h => h.HeadingType == HeadingType.Earnings)
                .Sum(h => h.Amount);

            var totalDeductions = employee.Headings
                .Where(h => h.HeadingType == HeadingType.Deductions)
                .Sum(h => h.Amount);

            var totalNetAmount = totalEarnings - totalDeductions;

            // INSS
            var baseINSS = _inssCalc.ComputeBaseINSS(employee);
            var deductionINSS = _inssCalc.ComputeDeductionINSS(baseINSS);
            // IR
            var deductionDependents = _irCalc.ComputeDeductionDependents(employee.Dependents);
            var baseIR = _irCalc.ComputeBaseIR(employee, deductionDependents, deductionINSS);            
            var deductionIR = _irCalc.ComputeDeductionIR(baseIR);
            
            var netAmount = GetNetAmount(totalNetAmount, deductionIR, deductionINSS);

            return new PayrollResult
            {
                Name = employee.Name,
                CPF = employee.CPF,
                BaseINSS = baseINSS,
                BaseIR = baseIR,
                DependentDeduction = deductionDependents,
                DeductionINSS = deductionINSS,
                DeductionIR = deductionIR,
                NetAmount = netAmount
            };
        }

        private decimal GetNetAmount(decimal totalNetAmount, decimal deductionIR, decimal deductionINSS)
        {
            return totalNetAmount - deductionIR - deductionINSS;
        }
    }
}
