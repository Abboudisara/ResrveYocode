using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reservation.Data;
using Reservation.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.Controllers
{
    [Authorize(Roles ="Admin")]
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



        /// ///////////////////////////////////User
        


        //UserRole
        [HttpGet]
        public async Task<IActionResult> EditeUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;

            if (string.IsNullOrEmpty(roleId))
            {
                return View("../Errors/NotFound", $"The role must be exist and not empty in Url");

            }
            var role = await roleManager.FindByIdAsync(roleId);
            if (role is null)
            {
                return View("../Errors/NotFound", $"The role Id : {role.Id} cannot be found");
            }

            var Models = new List<UserRoleViewModel>();
            foreach (var user in await UserManager.Users.ToListAsync())
            {
                UserRoleViewModel model = new UserRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    //IsSelected = false
                };
                if (await UserManager.IsInRoleAsync(user, role.Name))
                {
                    model.IsSelected = true;
                }
                else
                {
                    model.IsSelected = false;
                }

                Models.Add(model);
            }
            ViewBag.RoleId = roleId;
            return View(Models);

        }

  
        [HttpPost]
        public async Task<IActionResult> EditeUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                return View("../Errors/NotFound", $"The role must be exist and not empty in Url");

            }
            var role = await this.roleManager.FindByIdAsync(roleId);
            if (role is null)
            {
                return View("../Errors/NotFound", $"The role Id : {role.Id} cannot be found");
            }

            // role if deja affectté et in model is select il faut le supprimer , ou l'affecté si il est selecté au model mais non affecté before

            IdentityResult result = null;

            for (int i = 0; i < model.Count; i++)
            {
                ApplicationUser user = await UserManager.FindByIdAsync(model[i].UserId);

                if (await UserManager.IsInRoleAsync(user, role.Name) && !model[i].IsSelected)
                {
                    result = await UserManager.RemoveFromRoleAsync(user, role.Name);
                }
                else if (!(await UserManager.IsInRoleAsync(user, role.Name)) && model[i].IsSelected)
                {
                    result = await UserManager.AddToRoleAsync(user, role.Name);
                }
            }

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                }

            }
            return RedirectToAction("EditeRole", new { id = roleId });

        }


    }
}

//modification:
















public class ApplicationUser : IdentityUser
{
    
    public string Name { get; set; }
    
    public string FullName { get; set; }

    public int Counter { get; set; }

    public virtual List<Reserve> Reservations { get; set; }
}