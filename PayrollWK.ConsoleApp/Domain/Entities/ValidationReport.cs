using System.Text;

namespace PayrollWK.ConsoleApp.Domain.Entities
{
    public class ValidationReport
    {
        public int EmployeeIndex { get; set; }
        public int HeadingIndex { get; set; }
        public List<string> Errors { get; set; } = [];

        public override string ToString()
        {
            var builder = new StringBuilder();
            if (HeadingIndex == 0)
            {
                builder.AppendLine($"Employee {EmployeeIndex} - Erros encontrados:");

                foreach (var error in Errors)
                    builder.AppendLine($"  • {error}");
            }
            else if (EmployeeIndex > 0 && HeadingIndex > 0)
            {
                builder.AppendLine($"Employee {EmployeeIndex}");
                builder.AppendLine($"Heading {HeadingIndex} - Erros encontrados:");

                foreach (var error in Errors)
                    builder.AppendLine($"  • {error}");
            }            

            return builder.ToString();
        }
    }

}
