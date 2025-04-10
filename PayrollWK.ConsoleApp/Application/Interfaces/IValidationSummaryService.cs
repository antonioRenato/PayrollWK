using PayrollWK.ConsoleApp.Domain.Entities;

namespace PayrollWK.ConsoleApp.Application.Interfaces
{
    public interface IValidationSummaryService
    {
        void AddReport(int employeeIndex, int headingIndex, IEnumerable<string> errors);
        void PrintAllReports();
    }
}
