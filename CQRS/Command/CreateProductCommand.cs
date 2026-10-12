using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace ASPNETMVC.CQRS.Command
{
    public class CreateProductCommand
    {
        public string ProdName {get; set;}

        public decimal price {get; set;}

        public string shortDecription {get; set;}

        public string Description {get; set;}
    }
}