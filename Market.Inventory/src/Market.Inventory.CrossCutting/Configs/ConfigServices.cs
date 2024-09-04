using Market.Inventory.Application.Interfaces;
using Market.Inventory.Application.Services;
using Microsoft.Extensions.DependencyInjection;


namespace Market.Inventory.CrossCutting.Configs
{
    public static class ConfigServices
    {
        public static IServiceCollection ConfigAppServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}