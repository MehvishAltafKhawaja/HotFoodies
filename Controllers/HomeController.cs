using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASPNETMVC.Models;
using ASPNETMVC.Helpers;
using ASPNETMVC.Helpers;
using Microsoft.AspNetCore.Authorization;

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


   
    
    [Authorize]
    public IActionResult FeedBack()
    {   
        return View();
    }

    [Authorize]
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
     public IActionResult Privacy()
    {
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