using PayrollWK.ConsoleApp.Domain.Enums;

namespace PayrollWK.ConsoleApp.Domain.Entities
{
    public class Heading
    {
        public string Id { get; set; } = string.Empty;
        public int Code { get; set; }
        public string Description { get; set; } = string.Empty;
        public HeadingType HeadingType { get; set; }
        public decimal Amount { get; set; }
    }
}
