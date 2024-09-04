using Market.Sales.Domain.Interfaces;
using Market.Sales.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Market.Sales.IoC.Configuration
{
    public static class ConfigDependencyInjection
    {
           public static IServiceCollection ConfigAppDependencyInjection(this IServiceCollection services) 
            {
                services.AddScoped<ISaleRepository, SaleRepository>();

                return services;

            }  
    }
}