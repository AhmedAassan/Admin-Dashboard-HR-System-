using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Progect.BL.Models;
using Progect.DAL.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3TierArchitecture.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUserExtend> userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUserExtend> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
           var Roles = roleManager.Roles;
            return View(Roles);
        }




        #region Create
        public IActionResult Create()
        {

            return View();
        }




        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var role = new IdentityRole
                    {
                        Name = model.Name,
                        NormalizedName = model.Name.ToUpper()
                    };

                    var resulte = await roleManager.CreateAsync(role);
                    if (resulte.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var item in resulte.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
                return View(model);
            }
            catch
            {

                return View(model);
            }


        }
        #endregion



        #region Edit
        public async Task<IActionResult> Edit(string id)
        {
            var Role =await roleManager.FindByIdAsync(id);
            return View(Role);
        }




        [HttpPost]
        public async Task<IActionResult> Edit(IdentityRole model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Role = await roleManager.FindByIdAsync(model.Id);

                    Role.Name = model.Name;

                    var resulte = await roleManager.UpdateAsync(Role);

                    if (resulte.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var item in resulte.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }   
                    }


                }
                return View(model);
            }
            catch
            {

                return View(model);
            }


        }
        #endregion




        #region Details
        public async Task<IActionResult> Details(string id)
        {
            var Role = await roleManager.FindByIdAsync(id);
            return View(Role);
        }

        #endregion




        #region Delete
        public async Task<IActionResult> Delete(string id)
        {
            var Role = await roleManager.FindByIdAsync(id);
            return View(Role);
        }




        [HttpPost]
        public async Task<IActionResult> Delete(IdentityRole model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Role = await roleManager.FindByIdAsync(model.Id);

                    var resulte = await roleManager.DeleteAsync(Role);

                    if (resulte.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var item in resulte.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }


                }
                return View(model);
            }
            catch
            {

                return View(model);
            }


        }
        #endregion



        #region AddOrRemoveUsers
        


        public async Task<IActionResult> AddOrRemoveUsers(string RoleId)
        {

            ViewBag.RoleId = RoleId;

            var role = await roleManager.FindByIdAsync(RoleId);

            var model = new List<UserInRoleVM>();

            foreach (var user in userManager.Users)
            {
                var userInRole = new UserInRoleVM()
                {
                    UserId = user.Id,
                    UserName = user.Email
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userInRole.IsSelected = true;
                }
                else
                {
                    userInRole.IsSelected = false;
                }

                model.Add(userInRole);
            }

            return View(model);

        }


        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(List<UserInRoleVM> model, string RoleId)
        {

            var role = await roleManager.FindByIdAsync(RoleId);

            for (int i = 0; i < model.Count; i++)
            {

                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {

                    result = await userManager.AddToRoleAsync(user, role.Name);

                }
                else if (!model[i].IsSelected && (await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (i < model.Count)
                    continue;
            }

            return RedirectToAction("Edit", new { id = RoleId });
        }


        #endregion

    }
}
