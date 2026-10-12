using ASPNETMVC.Data;
using ASPNETMVC.Models;

namespace ASPNETMVC.CQRS.Command
{
    public class CreateProductHandler
    {
        private readonly ProductDbContext context;

        public CreateProductHandler(ProductDbContext context)
        {
            this.context = context;
        }

        public async Task Handle(CreateProductCommand command)
        {
            Product prd = new Product
            {
                ProdName = command.ProdName,
                price = command.price,
                shortDecription = command.shortDecription,
                Description = command.Description
            };

            context.Products.Add(prd);

            await context.SaveChangesAsync();
        }
    }
}