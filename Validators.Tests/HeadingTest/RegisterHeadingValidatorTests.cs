using PayrollWK.ConsoleApp.Domain.Enums;
using PayrollWK.ConsoleApp.Application.UseCases.HeadingRules;
using PayrollWK.ConsoleApp.Domain.Entities;

namespace Validators.Tests.HeadingTest
{
    public class RegisterHeadingValidatorTests
    {
        [Fact]
        public void Sucess()
        {
            var validator = new RegisterHeadingValidator();

            var heading = new Heading
            {
               Id = "RUB",
               Amount = 100,
               Code = 1001,
               Description = "Salário Base",
               HeadingType =  HeadingType.Earnings,
            };

            var result = validator.Validate(heading);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Failed()
        {
            var validator = new RegisterHeadingValidator();

            var heading = new Heading
            {
                Id = "RUB",
                Amount = 100,
                Code = 100,
                Description = "Vale Alimentação",
                HeadingType = HeadingType.Earnings,
            };

            var result = validator.Validate(heading);

            Assert.False(result.IsValid);
        }
    }
}
