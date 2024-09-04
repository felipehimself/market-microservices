using Market.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Market.Sales.Infrastructure.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) 
    {

        public DbSet<Sale> Sales { get; set; }
    }
}