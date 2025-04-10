using PayrollWK.ConsoleApp.Domain.Entities;
using PayrollWK.ConsoleApp.Exception;

namespace PayrollWK.ConsoleApp.Application.UseCases.HeadingRules
{
    public class RegisterHeadingUseCase
    {
        public Heading Execute(Heading heading)
        {
            Validate(heading);

            return heading;
        }

        private void Validate(Heading heading)
        {
            var validator = new RegisterHeadingValidator();

            var result = validator.Validate(heading);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
