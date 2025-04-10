using PayrollWK.ConsoleApp.Domain.Enums;

namespace PayrollWK.ConsoleApp.Utils
{
    public class HeadingTypeMapper
    {
        public static HeadingType FromHeadingTypeToString(string input)
        {
            return input.Trim().ToUpperInvariant() switch
            {
                "P" => HeadingType.Earnings,
                "D" => HeadingType.Deductions,
                _ => HeadingType.Unknown,
            };
        }
    }
}
