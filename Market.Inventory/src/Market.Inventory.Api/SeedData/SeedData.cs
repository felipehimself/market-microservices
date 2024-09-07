using Market.Inventory.Infrastructure.Context;
using Market.Inventory.Infrastructure.Seeds;

namespace Market.Inventory.Api.SeedData
{
    public static class SeedData
    {
        public static void Seeder(this IApplicationBuilder app, bool isProduction)
        {
            if (!isProduction)
            {
                using var serviceScope = app.ApplicationServices.CreateScope();
                ProductSeed.Products(serviceScope.ServiceProvider.GetService<AppDbContext>()!);

            }
        }
    }
}