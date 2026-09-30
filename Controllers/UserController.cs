using ASPNETMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ASPNETMVC.ViewModel;
using Microsoft.CodeAnalysis.Elfie.Serialization;
namespace ASPNETMVC.Controllers
{
    public class UserController : Controller
    {  
       public readonly UserManager<Users> userManager;
       public readonly SignInManager<Users> signInManager;
       private readonly IWebHostEnvironment env;
       
       public UserController(UserManager<Users> userManager,SignInManager<Users> signInManager ,IWebHostEnvironment env)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.env = env;
        }
        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {   
             if(ModelState.IsValid)
            {
                if(model.ImageUrl != null)
                {
                    string fn = Guid.NewGuid().ToString()+ "_"+ model.ImageUrl.FileName;
                    string folder = Path.Combine(env.WebRootPath, "UsersImages");
                    string imagepath = Path.Combine(folder, fn);

                    await model.ImageUrl.CopyToAsync(new FileStream(imagepath, FileMode.Create));

                    Users urs = new Users()
                    {
                        Name = model.Name,
                        Email = model.Email,
                        NormalizedEmail = model.Email,
                        UserName = model.Name,
                        NormalizedUserName = model.Name,
                        EmpGender = model.EmpGender,
                        ImageUrl = fn
                    };
                    var res = await userManager.CreateAsync(urs,model.password);
                    if(res.Succeeded)
                    {
                        return RedirectToAction("Index","Home");
                    }
                    else
                    {
                        foreach(var err in res.Errors)
                        {
                            ModelState.AddModelError("",err.Description);
                        }
                    }

                }
                else
                {
                    TempData["ErrorMessage"]= "Please Select Image!";
                }

            }


            return View(model);
        }
    }
}