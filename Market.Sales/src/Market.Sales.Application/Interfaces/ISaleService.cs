using Market.Sales.Application.Dtos;

namespace Market.Sales.Application.Interfaces
{
    public interface ISaleService
    {
        Task CreateSaleAsync(SaleCreateDto sale);
        Task<SaleReadDto?> GetSaleAsync(Guid id);
    }
}