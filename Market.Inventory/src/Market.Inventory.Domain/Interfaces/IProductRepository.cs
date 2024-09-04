
using Market.Inventory.Domain.Entities;

namespace Market.Inventory.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task <bool> SaveChangesAsync();
        Task UpdateProductAsync();

        Task<Product> CreateProductAsync(Product product);
        Task<IEnumerable<Product>> GetProductsAsync();

        Task<Product?> GetProductAsync(Guid id);
    }
}