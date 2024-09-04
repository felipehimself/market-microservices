using Market.Sales.Domain.Entities;
using Market.Sales.Domain.Interfaces;
using Market.Sales.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Market.Sales.Infrastructure.Repositories
{
    public class SaleRepository(AppDbContext context) : ISaleRepository
    {
        private readonly AppDbContext _context = context;

        public async Task CreateSaleAsync(Sale sale)
        {
            ArgumentNullException.ThrowIfNull(sale);
            sale.CreatedAt = DateTime.UtcNow;
            await _context.Sales.AddAsync(sale);
        }

        public async Task<Sale?> GetSaleAsync(Guid id)
        {
            return await _context.Sales.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}