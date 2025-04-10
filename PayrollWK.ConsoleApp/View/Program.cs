using Microsoft.Extensions.DependencyInjection;
using PayrollWK.ConsoleApp.Application.Interfaces;
using PayrollWK.ConsoleApp.Infrastructure.Config;
using PayrollWK.ConsoleApp.Infrastructure.Reader;

public class Program
{
    public static void Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddApplicationServices();

        using var serviceProvider = services.BuildServiceProvider();

        var payrollCalculator = serviceProvider.GetRequiredService<IPayrollCalcService>();
        
        PayrollFileReader reader = new PayrollFileReader();

        var employees = reader.ReadFile(@"C:\\teste\teste-texto.csv");

        foreach (var employee in employees.OrderBy(e => e.CPF))
        {
            var result = payrollCalculator.Calculate(employee);

            Console.WriteLine($@"
                                CPF: {result.CPF}
                                Nome: {result.Name}
                                Base INSS: {result.BaseINSS:C}
                                Base IR: {result.BaseIR:C}
                                Desconto Dependentes: {result.DependentDeduction:C}
                                Desconto INSS: {result.DeductionINSS:C}
                                Desconto IR: {result.DeductionIR:C}
                                Valor Líquido: {result.NetAmount:C}
                                -----------------------------");
        }

    }
}

