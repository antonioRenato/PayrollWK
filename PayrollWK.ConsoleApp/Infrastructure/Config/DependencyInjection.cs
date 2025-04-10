using Microsoft.Extensions.DependencyInjection;
using PayrollWK.ConsoleApp.Application.Interfaces;
using PayrollWK.ConsoleApp.Application.Services;

namespace PayrollWK.ConsoleApp.Infrastructure.Config
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IINSSCalcService, INSSCalcService>();
            services.AddScoped<IIRCalcService, IRCalcService>();
            services.AddScoped<IPayrollCalcService, PayrollCalcService>();

            return services;
        }
    }
}
