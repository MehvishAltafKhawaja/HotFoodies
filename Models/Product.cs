using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace ASPNETMVC.Models
{
    public class Product
    {   
        [Key]
        public int ProdId {get; set;}

        public string ProdName {get; set;}

        public decimal price {get; set;}

        public string shortDecription {get; set;}

        public string Description {get; set;}
    }
}