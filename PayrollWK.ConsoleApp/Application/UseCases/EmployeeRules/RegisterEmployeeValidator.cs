using FluentValidation;
using PayrollWK.ConsoleApp.Domain.Entities;

namespace PayrollWK.ConsoleApp.Application.UseCases.EmployeeRules
{
    public class RegisterEmployeeValidator : AbstractValidator<Employee>
    {
        public RegisterEmployeeValidator()
        {
            RuleFor(e => e.CPF).NotEmpty().WithMessage("The CPF is required!");
            RuleFor(e => e.CPF).Matches(@"^\d{11}$").WithMessage("CPF must be 11 digits long");
            RuleFor(e => e.Name).NotEmpty().WithMessage("The name is required!");
            RuleFor(e => e.Name).MaximumLength(40).MinimumLength(1).WithMessage("Name is too long (maximum: 40 characters)");
            RuleFor(e => e.Dependents).NotEmpty().WithMessage("The dependents is required!");
            RuleFor(e => e.Dependents).GreaterThan(0)
                .LessThan(100)
                .WithMessage("Dependents count must be entered with at least 2 digits");
        }
    }
}
