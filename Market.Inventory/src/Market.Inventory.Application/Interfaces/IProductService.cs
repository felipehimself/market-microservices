using Market.Inventory.Application.Dtos;

namespace Market.Inventory.Application.Interfaces
{
    public interface IProductService
    {
        ProductReadDto CreateProduct(ProductCreateDto productDto);

        ProductReadDto GetProduct(Guid id);

        IEnumerable<ProductReadDto> GetProducts();

        void UpdateProductInInventory(Guid id, int quantity);
    }
}