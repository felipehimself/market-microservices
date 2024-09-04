using Market.Inventory.Infrastructure.Context;
using Market.Inventory.Domain.Entities;
using Market.Inventory.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Market.Inventory.Infrastructure.Repositories
{
    public class ProductRepository(AppDbContext context) : IProductRepository
    {

        private readonly AppDbContext _context = context;

        public async Task<Product> CreateProductAsync(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);

            product.CreatedAt = DateTime.UtcNow;

            await _context.Products.AddAsync(product);

            return product;

        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }


        public async Task UpdateProductAsync()
        {
            throw new NotImplementedException();
        }


        public async Task<Product?> GetProductAsync(Guid id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }
        
        public async Task<bool> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync() >= 0;

        }

    }
}