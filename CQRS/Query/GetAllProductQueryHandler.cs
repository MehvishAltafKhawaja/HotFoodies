using ASPNETMVC.Data;
using ASPNETMVC.Models;
using Microsoft.EntityFrameworkCore;
using ASPNETMVC.CQRS.Query;

namespace ASPNETMVC.CQRS.Query
{
    public class GetAllProductQueryHandler
    {
        private readonly ProductDbContext context;

        public GetAllProductQueryHandler(ProductDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Product>> Handle(
            GetAllProductQuery query)
        {
            return await context.Products
                .AsNoTracking()
                .ToListAsync();
        }
    }
}