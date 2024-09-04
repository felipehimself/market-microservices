using Market.Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Market.Inventory.Infrastructure.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
    }
}