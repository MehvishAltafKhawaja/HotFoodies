using ASPNETMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ASPNETMVC.ViewModel;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using ASPNETMVC.Helpers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;
using ASPNETMVC.ViewModel;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace ASPNETMVC.Controllers
{
    public class UserController : Controller
    {
        public readonly UserManager<Users> userManager;
        public readonly SignInManager<Users> signInManager;
        private readonly IWebHostEnvironment env;

        public UserController(UserManager<Users> userManager, SignInManager<Users> signInManager, IWebHostEnvironment env)
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
                        string msg = "Dear" + model.Name + "<br/><br/>You have successfully Register on our HotFoodies.</br><b>User Id / Email :<b>" + model.Name + " or " + model.Email + "<br/><b> Password  :<b>" + model.password + ".<br/><br/>Regrading <br/><font color='green' size='10px'> Hot-Foodies Officail</font> <br/>";
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
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Users? user;

            user = await userManager.FindByEmailAsync(model.EmailOrUsername);

            if (user == null)
            {
                user = await userManager.FindByNameAsync(model.EmailOrUsername);
            }

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid Email/Username or Password");
                return View(model);
            }

            var result = await signInManager.PasswordSignInAsync(
               user,
               model.Password,
               model.RememberMe,
               lockoutOnFailure: false
            );
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid Email/Username or Password");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            if (signInManager.IsSignedIn(User))
            {
                await signInManager.SignOutAsync();
                return RedirectToAction("Login");
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> UpdatesPassword()
        {
            if (!signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login");
            }

            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var model = new UpdatePassViewModel
            {
                Email = user.Email
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatesPassword(UpdatePassViewModel model)
        {
            var user = await userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Password Updated!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ErrorMessage"] = "Server Error";
                }
            }


            return View(model);

        }


        // Forgot possword 

        // This is Used For Sand Reset Email view in web app
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        // This is Sand Forgot Passsword Email Post Method
        public async Task SendForgotPasswordEmail(string? email, Users? user)
        {

            // Generate password reset token
            var token = await userManager
         .GeneratePasswordResetTokenAsync(user);

            // Encode token for URL transport
            var encodedToken = WebEncoders.Base64UrlEncode(
                  Encoding.UTF8.GetBytes(token)
                );

            // Generate complete reset password URL
            var passwordResetLink = Url.Action(
               action: "ResetPassword",
               controller: "User",
               values: new
               {
                   Token = encodedToken,
                   Email = user.Email
               },
              protocol: Request.Scheme
             );

            if (passwordResetLink == null)
            {
                throw new InvalidOperationException(
                    "Unable to generate password reset link."
                );
            }

            // Encode URL for HTML email
            var safeLink = HtmlEncoder.Default.Encode(
                passwordResetLink
            );


            // Create the email Subject

            var subject = "Reset Your Password";

            // Creaye the message
            var messagebody = $@"
           <div style='background:#050b0c;
                padding:40px 15px;
                font-family:Arial,sans-serif;'>

        <div style='max-width:600px;
                    margin:auto;
                    background:#0b151b;
                    border:1px solid #203540;
                    border-radius:16px;
                    overflow:hidden;'>

            <div style='background:#073328;
                        padding:30px;
                        text-align:center;'>

                <h1 style='color:#ffffff;
                           margin:0;
                           font-size:30px;'>
                    Hot<span style='color:#00d879;'>
                    Foodies</span>
                </h1>

                <p style='color:#a9c9c4;'>
                    Secure Account Recovery
                </p>
            </div>

            <div style='padding:35px 30px;'>

                <h2 style='color:#ffffff;'>
                    Reset Your Password
                </h2>

                <p style='color:#b7c9d2;
                          font-size:15px;
                          line-height:1.8;'>
                    Hello, {user.Name}
                    <br/><br/>
                    We received a request to reset the
                    password for your HotFoodies account.
                </p>

                <p style='color:#b7c9d2;
                          font-size:15px;
                          line-height:1.8;'>
                    Click the button below to create
                    a new password.
                </p>

                <div style='text-align:center;
                            margin:35px 0;'>

                    <a href='{safeLink}'
                       style='background:#00c875;
                              color:#ffffff;
                              padding:16px 35px;
                              text-decoration:none;
                              border-radius:10px;
                              font-weight:bold;
                              display:inline-block;'>

                        Reset My Password

                    </a>
                </div>

                <p style='color:#b7c9d2;
                          font-size:14px;
                          line-height:1.7;'>
                    If you did not request this
                    password reset, you can safely
                    ignore this email.
                </p>

                <div style='border-top:1px solid #203540;
                            margin-top:30px;
                            padding-top:20px;'>

                    <p style='color:#78919d;
                              font-size:12px;'>
                        For your security, do not
                        share this reset link with anyone.
                    </p>
                </div>
            </div>

            <div style='background:#071116;
                        padding:20px;
                        text-align:center;'>

                <p style='color:#78919d;
                          font-size:12px;
                          margin:0;'>
                    &copy; {DateTime.UtcNow.Year}
                    HotFoodies. All rights reserved.
                </p>
            </div>
        </div>
    </div>";

            //Send Email 
            EmailHelper sender = new EmailHelper();
            sender.SendMail(email, subject, messagebody);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    await SendForgotPasswordEmail(user.Email, user);
                    return RedirectToAction("ForgotPasswordConfirmation", "User");
                }
            }
            else
            {
                ViewData["ErrorMessage"] = "SeverError";
                return RedirectToAction("ForgotPassword", "User");
            }
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string? Token, string? Email)
        {
            if (string.IsNullOrEmpty(Token) ||
                string.IsNullOrEmpty(Email))
            {
                ViewBag.ErrorTitle = "Invalid Password Reset Token";
                ViewBag.ErrorMessage = "The link is invalid or incomplete.";

                return View("Error");
            }

            ResetPasswordViewModel model = new ResetPasswordViewModel();

            model.Token = Token;
            model.Email = Email;

            return View(model);
        }



        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(
            ResetPasswordViewModel model)
        {
            // Step 1: Validate form
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Step 2: Check password confirmation
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError(
                    "ConfirmPassword",
                    "Password and Confirm Password do not match."
                );

                return View(model);
            }

            // Step 3: Find user by email
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                // Do not reveal whether the account exists
                return RedirectToAction("ResetPasswordConfirmation");
            }

            // Step 4: Decode reset token
            string decodedToken;

            try
            {
                decodedToken = Encoding.UTF8.GetString(
                    WebEncoders.Base64UrlDecode(model.Token)
                );
            }
            catch (FormatException)
            {
                ModelState.AddModelError(
                    "",
                    "Invalid password reset token."
                );

                return View(model);
            }

            // Step 5: Reset password using Identity
            var result = await userManager.ResetPasswordAsync(
                user,
                decodedToken,
                model.Password
            );

            // Step 6: Check result
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] =
                    "Your password has been reset successfully.";

                return RedirectToAction(
                    "ResetPasswordConfirmation",
                    "User"
                );
            }

            // Step 7: Display Identity errors
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(
                    "",
                    error.Description
                );
            }

            return View(model);
        }




        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

    }
}