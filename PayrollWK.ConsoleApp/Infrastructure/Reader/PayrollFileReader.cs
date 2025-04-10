using PayrollWK.ConsoleApp.Application.UseCases.EmployeeRules;
using PayrollWK.ConsoleApp.Domain.Entities;
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
            var lines = File.ReadAllLines(filePath);
            var employees = new List<Employee>();
            Employee? currentEmployee = null;

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

                    // UseCases Validators
                    var useCase = new RegisterEmployeeUseCase();
                    currentEmployee = useCase.Execute(currentEmployee);

                    employees.Add(currentEmployee);
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

                    currentEmployee.Headings.Add(heading);
                }
            }

            return employees.OrderBy(e => e.CPF).ToList();
        }
    }
}
