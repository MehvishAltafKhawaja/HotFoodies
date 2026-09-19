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
    
    [Route("AboutUs")]
     public IActionResult Aboutus()
    {   
        Data dt = new Data()
        {
         name = "Ubaid"
        };
        ViewData["Name"] = dt.name;
        return View(dt);
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


    [Route("Validation")]
     public IActionResult Validation()
    {
        return View();
    }
    
    [HttpPost]
     public IActionResult Validation(valid vd)
    {   if(ModelState.IsValid)
        {
            return RedirectToAction("ShowValid", vd);
        }
        return View(vd);
    }
    
    public IActionResult ShowValid(valid vd)
    {
        return View(vd);
    }
    public IActionResult Log()
    {
        log lg = new log()
        {
            username="ubaid",
            password="ubaid@123"
        };
        return View(lg);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
