using Market.Sales.Application.Dtos;

namespace Market.Sales.Application.Interfaces
{
    public interface ISaleService
    {
        Task<SaleReadDto> CreateSaleAsync(SaleCreateDto sale);
        Task<SaleReadDto?> GetSaleAsync(Guid id);
    }
}