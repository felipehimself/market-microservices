using Market.Inventory.Infrastructure.Repositories;
using Market.Inventory.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Market.Inventory.CrossCutting.Configs
{
    public static class ConfigDependencyInjection
    {
            public static IServiceCollection ConfigAppDependencyInjection(this IServiceCollection services) 
            {
                services.AddScoped<IProductRepository, ProductRepository>();

                return services;

            }
    }
}