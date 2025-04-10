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
            decimal total = 0m;

            if (baseINSS > 4190.83m)
                total += (baseINSS - 4190.83m) * 0.14m;

            if (baseINSS > 2793.88m)
                total += (baseINSS > 4190.83m ? 4190.83m : baseINSS) - 2793.88m * 0.12m;

            if (baseINSS > 1518.00m)
                total += (baseINSS > 2793.88m ? 2793.88m : baseINSS) - 1518.00m * 0.09m;

            if (baseINSS > 0)
                total += (baseINSS > 1518.00m ? 1518.00m : baseINSS) * 0.075m;

            return Math.Round(total, 2);
        }
    }
}
