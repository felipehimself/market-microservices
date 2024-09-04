using Market.Sales.Application.Interfaces;
using Market.Sales.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Market.Sales.IoC.Configuration
{
    public static class ConfigServices
    {
        public static IServiceCollection ConfigAppServices(this IServiceCollection services) 
        {
            services.AddScoped<ISaleService, SaleService>();

            return services;
        }
    }
}