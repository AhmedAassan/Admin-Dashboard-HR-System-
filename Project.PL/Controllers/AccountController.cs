using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Progect.BL.Helper;
using Progect.BL.Models;
using Progect.DAL.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3TierArchitecture.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUserExtend> userManager;
        private readonly SignInManager<IdentityUserExtend> signInManager;

        public AccountController(UserManager<IdentityUserExtend> userManager,SignInManager<IdentityUserExtend> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        #region Registration  (Sign up)


        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new IdentityUserExtend()
                    {
                        UserName = model.Name,
                        Email = model.Email,
                        IsAgree =model.IsAgree
                    };

                    var result = await userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }

                return View(model);
            }
            catch (Exception)
            {

                return View(model);
            }
            
        }
        #endregion








        #region Login  (Sign in)
        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByEmailAsync(model.Email);
                    var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid UserName Or Password");

                    }
                }

                return View(model);
            }
            catch (Exception)
            {

                return View(model);
            }
        }
        #endregion





        #region LogOff  (Sign Out)
        [HttpPost]
        public async Task<IActionResult>  LogOff()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        
        #endregion






        #region Forget Password


        public IActionResult ForgetPassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult>  ForgetPassword(ForgetPasswordVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Get User By Email // make sure Email in database
                    var user = await userManager.FindByEmailAsync(model.Email);

                    if (user != null)
                    {
                        // Generate token 
                        var token = await userManager.GeneratePasswordResetTokenAsync(user);
                        // Generate Reset Link 
                        var passwordResetLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);

                        MailSender.SendMail(new MailVM{Mail = model.Email, Title = "ResetPassword", Massage = passwordResetLink });

                        //logger.Log(LogLevel.Warning, passwordResetLink);

                        return RedirectToAction("ConfirmForgetPassword");
                    }

                    return RedirectToAction("ConfirmForgetPassword");
                }

                return View(model);
            }
            catch (Exception)
            {

                return View(model);
            }
        }


        public IActionResult ConfirmForgetPassword()
        {
            return View();
        }
        #endregion



        #region Reset Password
        public IActionResult ResetPassword( string Email, string Token)
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByEmailAsync(model.Email);

                    if (user != null)
                    {
                        var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);

                        if (result.Succeeded)
                        {
                            return RedirectToAction("ConfirmResetPassword");
                        }

                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                        return View(model);
                    }

                    return RedirectToAction("ConfirmResetPassword");
                }

                return View(model);
            }
            catch (Exception)
            {

                return View(model);
            }
        }

        public IActionResult ConfirmResetPassword()
        {
            return View();
        }
        #endregion


    }
}
