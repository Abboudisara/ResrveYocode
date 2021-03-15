using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Reservation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public UserManager<ApplicationUser> UserManager { get; }

        public AdministrationController(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            UserManager = userManager;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName

                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administration");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditeRole(String id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id={id} Can not be Found";
                return View("Not Found");
            }
            var model = new EditeViewModel
            {
                id = role.Id,
                RoleName = role.Name
            };
            foreach (var user in UserManager.Users.ToList())
            {
                if (await UserManager.IsInRoleAsync(user, role.Name).ConfigureAwait(true))
                {
                    model.Users.Add(user.UserName);

                }
            }
            return View(model);



        }

        [HttpPost]
        public async Task<IActionResult> EditeRole(EditeViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id={model.id} Can not be Found";
                return View("Not Found");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

           
        }


        //UserRole
        [HttpGet]
        public async Task<IActionResult> EditeUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id={roleId} Can not be Found";
                return View("Not Found");
            }
            var model = new List<UserRoleViewModel>();
            foreach(var user in UserManager.Users.ToList())
            {
                var UserRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserNam = user.UserName,
                };
                if (await UserManager.IsInRoleAsync(user, role.Name).ConfigureAwait(true))
                {
                    UserRoleViewModel.IsSelected = true;
                }
                else
                {
                    UserRoleViewModel.IsSelected = false;
                }
                model.Add(UserRoleViewModel);
            }
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> EditeUsersInRole(List<UserRoleViewModel>model,string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id={roleId} Can not be Found";
                return View("Not Found");
            }

            for(int i = 0; i < model.Count; i++)
            {
                var user = await UserManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;

                if (model[i].IsSelected || (await UserManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await UserManager.AddToRoleAsync(user, role.Name);
                   
                }

                if (!model[i].IsSelected && (await UserManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await UserManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditeRole", new { id = roleId });
                }

            }

            return RedirectToAction("EditeRole", new { id = roleId });
        }



    }


}















public class ApplicationUser : IdentityUser
{
    public static object Create { get; internal set; }
}