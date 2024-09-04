using Market.Sales.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Market.Sales.CrossCutting.Configs
{
    public static class ConfigConnectionString
    {
        public static IServiceCollection ConfigAppConnectionString(this IServiceCollection services, IConfiguration config, bool isProduction)
        {

            if (isProduction) 
            {
                var connectionString = config.GetConnectionString("MktSalesConnectionString");
                services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));
                
            } else {
                services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMemDbSales"));
            }

            return services;

        }
    }
}