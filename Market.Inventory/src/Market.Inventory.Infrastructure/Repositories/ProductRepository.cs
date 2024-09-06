using Market.Inventory.Infrastructure.Context;
using Market.Inventory.Domain.Entities;
using Market.Inventory.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Market.Inventory.Infrastructure.Repositories
{
    public class ProductRepository(AppDbContext context) : IProductRepository
    {

        private readonly AppDbContext _context = context;

        public Product CreateProduct(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);

            product.CreatedAt = DateTime.UtcNow;

            _context.Products.Add(product);

            return product;

        }

        public IEnumerable<Product> GetProducts()
        {
            return [.. _context.Products];
        }


        public void UpdateProductInInventory(Guid id, int quantity)
        {

            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            ArgumentNullException.ThrowIfNull(product);

            var newQuantity = product.Quantity - quantity;

            product.Quantity = newQuantity;
            product.UpdatedAt = DateTime.UtcNow;

            _context.Products.Update(product);

        }


        public Product? GetProduct(Guid id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;

        }

    }
}