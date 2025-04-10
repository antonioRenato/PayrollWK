using PayrollWK.ConsoleApp.Application.UseCases.EmployeeRules;
using PayrollWK.ConsoleApp.Domain.Entities;
using PayrollWK.ConsoleApp.Domain.Enums;

namespace Validators.Tests.EmployeeTest
{
    public class RegisterEmployeeValidatorTests
    {
        [Fact]
        public void Sucess()
        {
            var validator = new RegisterEmployeeValidator();

            var employee = new Employee
            {
                CPF = "98765432100",
                Id = "EMP",
                Name = "Antonio Renato",
                Dependents = 2,
                Headings = [],
            };

            var result = validator.Validate(employee);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Failed()
        {
            var validator = new RegisterEmployeeValidator();

            var employee = new Employee
            {
                CPF = "98765432",
                Id = "EMP",
                Name = "Antonio Renato",
                Dependents = 2,
                Headings = [],
            };

            var result = validator.Validate(employee);

            Assert.False(result.IsValid);
        }
    }
}
