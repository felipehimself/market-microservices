using Market.Inventory.Domain.Entities;
using Market.Inventory.Infrastructure.Context;

namespace Market.Inventory.Infrastructure.Seeds
{
    public static class ProductSeed
    {

        public static void Products(AppDbContext dbContext)
        {
            var products = new List<Product>(){

                new ()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 1",
                    Quantity = 10,
                    Category = "Category 1",
                    Price = 10.00,
                    CreatedAt = DateTime.UtcNow
                },

                new ()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 2",
                    Quantity = 20,
                    Category = "Category 2",
                    Price = 20.00,
                    CreatedAt = DateTime.UtcNow
                },

                new ()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 3",
                    Quantity = 30,
                    Category = "Category 3",
                    Price = 30.00,
                    CreatedAt = DateTime.UtcNow
                }
                };

            dbContext.Products.AddRange(products);
            dbContext.SaveChanges();

        }
    }
}