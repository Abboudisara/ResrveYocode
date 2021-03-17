using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reservation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _identityRole;

        public AdminController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> identityRole)
        {
            _db = db;
            _userManager = userManager;
            _identityRole = identityRole;
        }
       
        public IActionResult Index()
        {
            // var display = _db.Reservations.ToList().Where(r => r.User_id == user.Id);

            var List = _db.Reservations.Include(c => c.utitlisateur);
            
            return View(List);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var getUserId = await _db.Reservations.FindAsync(id);
            return View(getUserId);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Reserve re)
        {
            if (ModelState.IsValid)
            {
                _db.Update(re);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(re);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var getUserId = await _db.Reservations.FindAsync(id);
            return View(getUserId);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            var getUserId = await _db.Reservations.FindAsync(id);
            _db.Reservations.Remove(getUserId);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }


}
