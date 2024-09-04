using Market.Inventory.Application.Dtos;

namespace Market.Inventory.Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductReadDto> CreateProductAsync(ProductCreateDto productDto);

        Task<ProductReadDto> GetProductAsync(Guid id);

        Task<IEnumerable<ProductReadDto>> GetProductsAsync();
    }
}