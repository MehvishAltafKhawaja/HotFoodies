using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASPNETMVC.Models;

namespace ASPNETMVC.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    
     public IActionResult Aboutus()
    {   
        Data dt = new Data()
        {
         name = "Ubaid"
        };
        ViewData["Name"] = dt.name;
        return View(dt);
    }
    public IActionResult Showproduct()
    {   
        // For Single Product or data 
        // Product prd = new Product()
        // {
        //      prodid= 1,
        //      prodname= "Iphone",
        //      prodprice= 20000,
        //      prodQty= 10
        // };
        // --------------------------------------------
// For Multi Data we List 

    List<Product> prd = new List<Product>()
    {
        new Product(){prodid=1, prodname="Iphone", prodprice=200000, prodQty=20},
         new Product(){prodid=2, prodname="Cycle", prodprice=20000, prodQty=10},
          new Product(){prodid=3, prodname="Laptop", prodprice=100000, prodQty=20},
           new Product(){prodid=4, prodname="LCD", prodprice=60000, prodQty=15},
    };

       return View(prd);
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
