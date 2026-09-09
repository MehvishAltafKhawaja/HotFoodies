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

    public IActionResult data()
    {   
        //  This Property is used to store data by Viewdata
        // ViewData["ProductId"] = 1;
        // ViewData["Productname"]= "Apple";
        // ViewData["ProductPrice"]= 500;
        // ViewData["ProductQty"]= 20;


        //  Now We use ViewBag

        ViewBag.Id = 10;
        ViewBag.Name = "Ubaid";
        ViewBag.Loaction= "HMT";
        ViewBag.School="ILS";
        ViewBag.Fee=12000;
        ViewBag.Rant= 4000;
        ViewBag.Bus= 2000;
        return View();
    }


    public IActionResult FirstPage()
    {
        TempData["Name"]= "Ubaid";
        TempData["Message"]= "Welcome Sir!";

        return RedirectToAction("SecondPgae");
    }
    public IActionResult SecondPgae()
    {
        return View();
    }
    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult FeedBack()
    {
        return View();
    }


     public IActionResult Standard()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
