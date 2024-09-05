using Market.Inventory.Application.Interfaces;
using Market.Inventory.Application.Services;
using Market.Inventory.CrossCutting.Logger;
using Microsoft.Extensions.DependencyInjection;


namespace Market.Inventory.IoC.Configuration
{
    public static class ConfigServices
    {
        public static IServiceCollection ConfigAppServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddSingleton(typeof (ILoggerService<>), typeof (LoggerService<>));
            return services;
        }
    }
}