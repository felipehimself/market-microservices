
using Market.Inventory.Domain.Entities;

namespace Market.Inventory.Domain.Interfaces
{
    public interface IProductRepository
    {
        bool SaveChanges();
        void UpdateProductInInventory(Guid id, int quantity);

        Product CreateProduct(Product product);
        IEnumerable<Product> GetProducts();

        Product? GetProduct(Guid id);
    }
}