using PayrollWK.ConsoleApp.Application.Interfaces;
using PayrollWK.ConsoleApp.Domain.Entities;
using PayrollWK.ConsoleApp.Domain.Enums;

namespace PayrollWK.ConsoleApp.Application.Services
{
    public class INSSCalcService : IINSSCalcService
    {
        public decimal ComputeBaseINSS(Employee employee)
        {
            var earn = employee.Headings
                .Where(h => h.HeadingType == HeadingType.Earnings)
                .Sum(h => h.Amount);

            var deduction = employee.Headings
                .Where(h => h.HeadingType == HeadingType.Deductions)
                .Sum(h => h.Amount);

            return earn - deduction;
        }

        public decimal ComputeDeductionINSS(decimal baseINSS)
        {
            if(baseINSS <= 1518.00m)
                return baseINSS * 0.075m;
            if (baseINSS <= 2793.88m)
                return baseINSS * 0.09m;
            if (baseINSS <= 4190.83m)
                return baseINSS * 0.12m;

            return baseINSS * 0.14m;
        }
    }
}
