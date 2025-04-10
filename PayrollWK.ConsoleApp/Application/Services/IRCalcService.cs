using PayrollWK.ConsoleApp.Application.Interfaces;
using PayrollWK.ConsoleApp.Domain.Entities;
using PayrollWK.ConsoleApp.Domain.Enums;
using System.Buffers.Text;

namespace PayrollWK.ConsoleApp.Application.Services
{
    public class IRCalcService : IIRCalcService
    {
        private const decimal MINIMUM_SALARY = 1518.00m;

        public decimal AmountPerDependent => MINIMUM_SALARY * 0.05m;

        private const decimal IR_RATE_LEVEL1 = 0.05m;
        private const decimal IR_RATE_LEVEL2 = 0.10m;
        private const decimal IR_RATE_LEVEL3 = 0.15m;
        private const decimal IR_RATE_LEVEL4 = 0.20m;

        public decimal ComputeBaseIR(Employee employee, decimal deductionDependents, decimal baseINSS)
        {
            var netPayAmount = GetTotalEarningsMinusDeductions(employee);

            return netPayAmount - deductionDependents - baseINSS;
        }

        private decimal GetTotalEarningsMinusDeductions(Employee employee)
        {
            var earn = employee.Headings
                            .Where(h => h.HeadingType == HeadingType.Earnings)
                            .Sum(h => h.Amount);

            var deduction = employee.Headings
                .Where(h => h.HeadingType == HeadingType.Deductions)
                .Sum(h => h.Amount);

            return earn - deduction;
        }

        public decimal ComputeDeductionDependents(int quantityDependents)
        {
            return quantityDependents * AmountPerDependent;
        }

        public decimal ComputeDeductionIR(decimal baseIR)
        {
            if (baseIR <= MINIMUM_SALARY * 2)
                return 0;
            if (baseIR <= MINIMUM_SALARY * 4)
                return baseIR * IR_RATE_LEVEL1;
            if (baseIR <= MINIMUM_SALARY * 5)
                return baseIR * IR_RATE_LEVEL2;
            if (baseIR <= MINIMUM_SALARY * 7)
                return baseIR * IR_RATE_LEVEL3;

            return baseIR * IR_RATE_LEVEL4;
        }
    }
}
