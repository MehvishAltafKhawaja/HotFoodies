using ASPNETMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETMVC.Controllers
{
    public class AdminController : Controller
    {  
        public IActionResult index()
        {
            return View();
        }
        public IActionResult ShowFeedBackData(Feedback fb)
        {   
            ViewBag.Name = fb.username;
            ViewBag.Message= fb.message;
            return View();
        }
        public IActionResult ShowData()
        {   TempData["Message"]= "Welcome to Show Data page Via Standerd Html Helper Class";
            return View();
        }
        public IActionResult AddProduct()
        {
            return View();
        }
    }
}