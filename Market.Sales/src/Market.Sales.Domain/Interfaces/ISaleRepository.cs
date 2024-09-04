using Market.Sales.Domain.Entities;

namespace Market.Sales.Domain.Interfaces
{
    public interface ISaleRepository
    {
       Task <bool> SaveChangesAsync(); 
       Task CreateSaleAsync(Sale sale);

       Task<Sale?> GetSaleAsync(Guid id);
    }
}