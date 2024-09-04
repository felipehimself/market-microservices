using Market.Inventory.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Market.Inventory.CrossCutting.Configs
{
    public static class ConfigConnectionString
    {
        public static IServiceCollection ConfigAppConnectionString(this IServiceCollection services, IConfiguration config, bool isProduction)
        {

            if (isProduction) 
            {
                var connectionString = config.GetConnectionString("MktIneventoryConnectionString");
                services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));
                
            } else {
                services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMemDb"));
            }

            return services;

        }
    }
}