using ASPNETMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETMVC.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult ShowProduct(addProduct prd)
        {   
            TempData["Prodname"]= prd.Pname;
            TempData["Prodprice"]= prd.Pprice;
            TempData["ProdId"]= prd.PId;
            TempData["Prodcategory"]= prd.Pcetogory;
            TempData["ProdType"]= prd.Plist;
            return View();
        }

        public IActionResult ValidSam()
        {
           return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(CreateProd vds)
        {
           if(ModelState.IsValid)
            {
                return RedirectToAction("Details",vds);
            }
            return View();
        }

        public IActionResult Details(CreateProd vds)
        {
            return View(vds);
        }

        // public IActionResult ProductList(CreateProd vds)
        // {
        //     return View(vds);
        // }
    }
    
}