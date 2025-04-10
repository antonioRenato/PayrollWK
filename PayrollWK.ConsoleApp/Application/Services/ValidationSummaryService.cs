using PayrollWK.ConsoleApp.Application.Interfaces;
using PayrollWK.ConsoleApp.Domain.Entities;

namespace PayrollWK.ConsoleApp.Application.Services
{
    public class ValidationSummaryService : IValidationSummaryService
    {
        private readonly List<ValidationReport> _reports = [];

        public void AddReport(int employeeIndex, int headingIndex, IEnumerable<string> errors)
        {
            _reports.Add(new ValidationReport
            {
                EmployeeIndex = employeeIndex,
                HeadingIndex = headingIndex,
                Errors = errors.ToList()
            });
        }

        public void PrintAllReports()
        {
            foreach (var report in _reports)
            {
                Console.WriteLine(report);
            }
        }
    }
}
