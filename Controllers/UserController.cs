using ASPNETMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ASPNETMVC.ViewModel;
using ASPNETMVC.ViewModel;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using ASPNETMVC.Helpers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
           if (ModelState.IsValid)
            {
                if (model.ImageUrl != null)
                {
                       string fn = Guid.NewGuid().ToString() + "_" +
                        model.ImageUrl.FileName;
                        string folder = Path.Combine(
                        env.WebRootPath,
                        "UsersImages"
                      );

                      // Create folder if it does not exist
                   if (!Directory.Exists(folder))
                       {
                           Directory.CreateDirectory(folder);
                        }

            string imagepath = Path.Combine(folder, fn);

            using (var stream = new FileStream(
                imagepath,
                FileMode.Create))
            {
                await model.ImageUrl.CopyToAsync(stream);
            }

            Users urs = new Users()
            {
                Name = model.Name,
                Email = model.Email,
                NormalizedEmail = model.Email.ToUpper(),
                UserName = model.Name,
                NormalizedUserName = model.Name.ToUpper(),
                EmpGender = model.EmpGender,
                ImageUrl = fn
            };

            var res = await userManager.CreateAsync(
                urs,
                model.password
            );

            if (res.Succeeded)
            {   
                EmailHelper emailHelper = new EmailHelper();
                string msg = "Dear" + model.Name + "<br/><br/>You have successfully Register on our HotFoodies.</br><b>User Id / Email :<b>" + model.Name+ " or "+ model.Email + "<br/><b> Password  :<b>" + model.password + ".<br/><br/>Regrading <br/><font color='green' size='10px'> Hot-Foodies Officail</font> <br/>";
                emailHelper.SendMail(model.Email, "Signup Completed Confirmation", msg);
                return RedirectToAction("Login", "User");
            }

            foreach (var err in res.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }
        }
            else
             {
                 ModelState.AddModelError(
                   "ImageUrl",
                   "Please Select Image!"
                );
            }
    }   return View(model);
 }

           [HttpGet]
            public IActionResult Login()
       {
                 return View();
             }

            [HttpPost]
            public async Task<IActionResult> Login(LoginViewModel model)
            {   
                if(!ModelState.IsValid)
                 {
                   return View(model);
                 }
                 Users? user;

                    user = await userManager.FindByEmailAsync(model.EmailOrUsername);

                 if(user == null)
                {
                   user = await userManager.FindByNameAsync(model.EmailOrUsername);
                }

                if (user == null)
                {
                   ModelState.AddModelError("","Invalid Email/Username or Password");
                   return View(model);
                 }

                 var result = await signInManager.PasswordSignInAsync(
                    user,
                    model.Password,
                    model.RememberMe,
                    lockoutOnFailure: false
                 );
                   if(result.Succeeded)
                   {
                     return RedirectToAction("Index","Home");
                   }

                   ModelState.AddModelError("", "Invalid Email/Username or Password");
                   return View(model);
            }
           
           public async Task<IActionResult> Logout()
        {
            if(signInManager.IsSignedIn(User))
            {
                await signInManager.SignOutAsync();
                return RedirectToAction("Login");
            }

            return NotFound();
        }
    }
} 