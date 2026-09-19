using ASPNETMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.CodeAnalysis.Scripting.Hosting;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ASPNETMVC.Controllers
{
    public class ProductController : Controller
    { 
        private readonly ILogger<ProductController> _logger;
        private readonly ProductDbContext context;
        public ProductController(ILogger<ProductController> logger , ProductDbContext context)
        {
            _logger=logger;
            this.context = context;
        } 
        
        public IActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateProduct(ProductTable Pdt)
        {
            if(ModelState.IsValid)
            {  try{
                context.ProductTables.Add(Pdt);
                context.SaveChanges();
                TempData["Success"]="Data Inserted Successfully!..";
                return RedirectToAction("ProductList");
            }
            catch(Exception ex)
                {   
                    TempData["ErrorMessage"]="Server Error Try Again!..";
                    return RedirectToAction("ProductList");
                }
            }
            return View(Pdt);
        }

        public IActionResult ProductList()
        {
            List<ProductTable> pdt = context.ProductTables.ToList();
            return View(pdt);
        }


        public IActionResult Details(int? id)
        {
            if(id != null)
            {
                ProductTable pdt = context.ProductTables.FirstOrDefault(item=>item.ProdId==id);
                if(pdt != null)
                {
                    return View(pdt);
                }
                else
                {   
                    TempData["ErrorMessage"] = id + " Not Found! ";
                    return RedirectToAction("ProductList");
                }
            }
            else
                {   TempData["ErrorMessage"] = "Please Entry ID to Check Details! ";
                    return RedirectToAction("ProductList");
                }

        }

        public IActionResult Update(int? id)
        {
            if(id!= null)
            {
                ProductTable pt = context.ProductTables.FirstOrDefault(item=>item.ProdId==id);
                if(pt!=null)
                {
                    return View(pt);
                }
                else
                {
                      TempData["ErrorMessage"] = id + " Not Found! ";
                    return RedirectToAction("ProductList");
                }

            }
            else
            {
                TempData["ErrorMessage"] = "Please Entry ID to Check Details! ";
                    return RedirectToAction("ProductList");
            }

        }

        [HttpPost]
        
        public IActionResult Update(ProductTable pt)
        {  try{
            if(ModelState.IsValid)
            {
                context.ProductTables.Update(pt);
                context.SaveChanges();
                TempData["Success"]="Data Update Successfully!..";
                return RedirectToAction("ProductList");
            }}
            catch(Exception ex)
            {
                TempData["ErrorMessage"]="Server Error Try Again!..";
                return RedirectToAction("ProductList");
            }

            return View(pt);
        }

        public IActionResult Delete(int? id)
        {   
            if(id!=null)
            {
                ProductTable pd = context.ProductTables.FirstOrDefault(item=>item.ProdId==id);
                if(pd!=null)
                {
                    return View(pd);
                }
                else
                {
                    TempData["ErrorMessage"]=id+ "Id Not Found!";
                    return RedirectToAction("ProductList");
                }
            }
            TempData["ErrorMessage"]="Please Enter Id!";
            return RedirectToAction("ProductList");
        }

        [HttpPost]
        public IActionResult Delete(ProductTable Pt)
        { try{
            if(Pt!=null)
            {
                context.ProductTables.Remove(Pt);
                context.SaveChanges();
                TempData["Success"]="Data Update Successfully!..";
                return RedirectToAction("ProductList");

            }
        }
        catch(Exception ex)
            {
                 TempData["ErrorMessage"]="Server Error!..";
                 return RedirectToAction("ProductList");
            }
            TempData["ErrorMessage"]="Some Error Occurs!";
            return RedirectToAction("ProductList");
        }
    }
    
}