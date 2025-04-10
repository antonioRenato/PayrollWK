using Microsoft.Extensions.DependencyInjection;
using PayrollWK.ConsoleApp.Application.Interfaces;
using PayrollWK.ConsoleApp.Application.UseCases.EmployeeRules;
using PayrollWK.ConsoleApp.Application.UseCases.HeadingRules;
using PayrollWK.ConsoleApp.Domain.Entities;
using PayrollWK.ConsoleApp.Exception;
using PayrollWK.ConsoleApp.Infrastructure.Config;
using PayrollWK.ConsoleApp.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollWK.ConsoleApp.Infrastructure.Reader
{
    public class PayrollFileReader
    {
        public List<Employee> ReadFile(string filePath)
        {
            var services = new ServiceCollection();
            services.AddApplicationServices();

            using var serviceProvider = services.BuildServiceProvider();

            var validationSummary = serviceProvider.GetRequiredService<IValidationSummaryService>();

            var lines = File.ReadAllLines(filePath);
            var employees = new List<Employee>();
            Employee? currentEmployee = null;
            var employeeIndex = 0;
            var headingIndex = 0;

            foreach (var line in lines)
            {
                var fields = line.Split(';');
                var registerType = fields[0];

                if (string.Equals(registerType, "EMP"))
                {
                    currentEmployee = new Employee
                    {
                        CPF = fields[1],
                        Name = fields[2],
                        Dependents = int.Parse(fields[3]),
                        Headings = new List<Heading>()
                    };

                    try
                    {
                        employeeIndex++;
                        headingIndex = 0;
                        // UseCases Validators Employee
                        var useCaseEmployee = new RegisterEmployeeUseCase();
                        currentEmployee = useCaseEmployee.Execute(currentEmployee);

                        employees.Add(currentEmployee);                       
                    }
                    catch (ErrorOnValidationException e)
                    {
                        validationSummary.AddReport(employeeIndex, 0, e.Errors);
                    }                    
                }
                else if (string.Equals(registerType,"RUB") && currentEmployee != null)
                {
                    var heading = new Heading
                    {
                        Code = int.Parse(fields[1]),
                        Description = fields[2],
                        HeadingType = HeadingTypeMapper.FromHeadingTypeToString(fields[3]),
                        Amount = Math.Truncate(decimal.Parse(fields[4], CultureInfo.InvariantCulture) * 100) / 100,
                    };

                    try
                    {
                        headingIndex++;

                        // UseCases Validators Heading
                        var useCaseHeading = new RegisterHeadingUseCase();
                        heading = useCaseHeading.Execute(heading);

                        currentEmployee.Headings.Add(heading);
                    }
                    catch (ErrorOnValidationException e)
                    {
                        validationSummary.AddReport(employeeIndex, headingIndex, e.Errors);
                    }
                }
            }

            validationSummary.PrintAllReports();

            return employees.OrderBy(e => e.CPF).ToList();
        }
    }
}
