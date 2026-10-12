
using Microsoft.EntityFrameworkCore;
using ASPNETMVC.Models;
namespace ASPNETMVC.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(
            DbContextOptions<ProductDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}