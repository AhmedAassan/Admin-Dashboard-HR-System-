using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Progect.DAL.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3TierArchitecture.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUserExtend> userManager;

        public UsersController(UserManager<IdentityUserExtend> userManager)
        {
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            var users = userManager.Users;
            return View(users);
        }




        [HttpGet]
        public async Task<IActionResult>  Edit( string id)
        {
            var data = await userManager.FindByIdAsync(id);
            return View(data);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(IdentityUserExtend model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByIdAsync(model.Id);

                    user.UserName = model.UserName;
                    user.Email = model.Email;

                    var result = await userManager.UpdateAsync(user);


                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
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










        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var data = await userManager.FindByIdAsync(id);
            return View(data);
        }



        [HttpPost]
        public async Task<IActionResult> Delete(IdentityUserExtend model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByIdAsync(model.Id);

                    

                    var result = await userManager.DeleteAsync(user);


                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
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




        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var data = await userManager.FindByIdAsync(id);
            return View(data);
        }
    }
}
