using Microsoft.Extensions.DependencyInjection;
using PayrollWK.ConsoleApp.Application.Interfaces;
using PayrollWK.ConsoleApp.Infrastructure.Config;
using PayrollWK.ConsoleApp.Infrastructure.Reader;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        var payrollCalculator = GetPayrollCalcService();

        PayrollFileReader reader = new PayrollFileReader();

        Console.Write("Digite o caminho do arquivo .csv: ");
        var filePath = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
        {
            Console.WriteLine("Arquivo não encontrado: " + filePath);
            return;
        }

        var employees = reader.ReadFile(filePath);
        var sb = new StringBuilder();

        foreach (var employee in employees)
        {
            var result = payrollCalculator.Calculate(employee);

            sb.AppendLine($"CPF: {result.CPF}");
            sb.AppendLine($"Nome: {result.Name}");
            sb.AppendLine($"Base INSS: {result.BaseINSS:C}");
            sb.AppendLine($"Base IR: {result.BaseIR:C}");
            sb.AppendLine($"Desconto Dependentes: {result.DependentDeduction:C}");
            sb.AppendLine($"Desconto INSS: {result.DeductionINSS:C}");
            sb.AppendLine($"Desconto IR: {result.DeductionIR:C}");
            sb.AppendLine($"Valor Líquido: {result.NetAmount:C}");
            sb.AppendLine("-----------------------------");
        }

        Console.WriteLine(sb.ToString());

        if (!string.IsNullOrWhiteSpace(filePath) && File.Exists(filePath))
            CreatePayrollReport(filePath, sb);
    }

    private static void CreatePayrollReport(string filePath, StringBuilder sb)
    {
        var fileName = $"relatorio-folha-{DateTime.Now:yyyyMMdd_HHmmss}.txt";
        var folderPath = Path.GetDirectoryName(filePath);

        if (Directory.Exists(folderPath))
        {
            var folderName = Path.Combine(folderPath, "Relatorios-Folha");
            Directory.CreateDirectory(folderName);
            var outputPath = Path.Combine(folderName, fileName);

            File.WriteAllText(outputPath, sb.ToString());

            Console.WriteLine($"Relatório salvo com sucesso em: {Path.GetFullPath(outputPath)}");
        }
    }

    private static IPayrollCalcService GetPayrollCalcService()
    {
        var services = new ServiceCollection();
        services.AddApplicationServices();

        using var serviceProvider = services.BuildServiceProvider();

        return serviceProvider.GetRequiredService<IPayrollCalcService>();
    }
}

