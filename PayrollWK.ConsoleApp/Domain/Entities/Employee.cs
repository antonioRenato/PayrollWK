namespace PayrollWK.ConsoleApp.Domain.Entities
{
    public class Employee
    {
        public string Id { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string Name {  get; set; } = string.Empty;
        public int Dependents { get; set; } = 0;

        public List<Heading> Headings { get; set; } = [];
    }
}
