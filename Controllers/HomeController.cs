using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASPNETMVC.Models;
using ASPNETMVC.Helpers;
using ASPNETMVC.Helpers;

namespace ASPNETMVC.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [Route("AboutUs")]
    public IActionResult AboutUs()
    {
        // This Property is used to store data by ViewData
        // ViewData["ProductId"] = 1;
        // ViewData["Productname"] = "Apple";
        // ViewData["ProductPrice"] = 500;
        // ViewData["ProductQty"] = 20;

        // Now we use ViewBag
        ViewBag.Id = 10;
        ViewBag.Name = "Ubaid";
        ViewBag.Loaction = "HMT";
        ViewBag.School = "ILS";
        ViewBag.Fee = 12000;
        ViewBag.Rant = 4000;
        ViewBag.Bus = 2000;

        return View();
    }

    public IActionResult FirstPage()
    {
        TempData["Name"] = "Ubaid";
        TempData["Message"] = "Welcome Sir!";

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
      public IActionResult SendMail(string to, string subject, string message)
    {
        EmailHelper helper = new EmailHelper();

        bool result = helper.SendMail(to, subject, message);

        if (result==true)
        {
            TempData["SuccessMessage"] = "Email sent successfully.";
            return RedirectToAction ("Index", "Home");
        }
        else
        {
            TempData["ErrorMessage"] = "Email could not be sent.";
            return RedirectToAction ("Index", "Home");
        }
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

    public IActionResult sessionform()
    {
        return View();
    }
    [HttpPost]
      public IActionResult sessionform(string nm)
    {   
        HttpContext.Session.SetString("Name",nm);
        ViewData["message"] = "Name Successfully added to session";
        return View();
    }
  [ResponseCache(
    Duration = 0,
    Location = Microsoft.AspNetCore.Mvc.ResponseCacheLocation.None,
    NoStore = true)]
public IActionResult Error()
{
    return View(new ErrorViewModel
    {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
    });
}
}