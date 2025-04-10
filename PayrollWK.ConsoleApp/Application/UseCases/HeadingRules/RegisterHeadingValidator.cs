using FluentValidation;
using PayrollWK.ConsoleApp.Domain.Entities;
using PayrollWK.ConsoleApp.Domain.Enums;
using System.Globalization;

namespace PayrollWK.ConsoleApp.Application.UseCases.HeadingRules
{
    public class RegisterHeadingValidator : AbstractValidator<Heading>
    {
        public RegisterHeadingValidator()
        {
            RuleFor(h => h.Code).NotEmpty().WithMessage("Code is required!");
            RuleFor(h => h.Code)
                .Must(amount => amount.ToString(CultureInfo.InvariantCulture).Length == 4)
                .WithMessage("The code of Heading must be at least 4 digits.");
            RuleFor(h => h.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(h => h.Description).MaximumLength(40).WithMessage("The Description must be less than 40 characters");
            RuleFor(h => h.HeadingType).NotEqual(HeadingType.Unknown).WithMessage("The type must be 'P' or 'D'");
            RuleFor(h => h.Amount).GreaterThan(0)
                .Must(amount => amount.ToString(CultureInfo.InvariantCulture).Length <= 14)
                .WithMessage("Amount must have at most 14 digits (including decimals).");
        }
    }
}
