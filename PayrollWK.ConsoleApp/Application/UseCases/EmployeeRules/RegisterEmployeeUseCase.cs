using PayrollWK.ConsoleApp.Domain.Entities;
using PayrollWK.ConsoleApp.Exception;

namespace PayrollWK.ConsoleApp.Application.UseCases.EmployeeRules
{
    public class RegisterEmployeeUseCase
    {
        public Employee Execute(Employee employee)
        {
            Validate(employee);

            return employee;
        }

        private void Validate(Employee employee)
        {
            var validator = new RegisterEmployeeValidator();

            var result = validator.Validate(employee);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
