namespace PayrollWK.ConsoleApp.Domain.Entities
{
    public class PayrollResult
    {
        public string CPF { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal BaseINSS { get; set; }
        public decimal BaseIR { get; set; }
        public decimal DependentDeduction { get; set; }
        public decimal DeductionINSS { get; set; }
        public decimal DeductionIR { get; set; }
        public decimal NetAmount { get; set; }
    }
}
