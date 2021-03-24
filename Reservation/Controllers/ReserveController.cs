using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Reservation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.Controllers
{
    [Authorize(Roles = "Apprenant")]
    public class ReserveController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _identityRole;

        public ReserveController(ApplicationDbContext db , UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> identityRole)
        {
            _db = db;
            _userManager = userManager;
            _identityRole = identityRole;
        }

        
        public async Task < IActionResult> Index()
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var display = _db.Reservations.ToList().Where(r=>r.User_id==user.Id);

            return View(display);
        }


        public IActionResult Create()
        {

            return View();
        }
        
        
        public IEnumerable<Reserve> display { get; set; }
        [HttpPost]
        public async Task<IActionResult> Create(Reserve nec)
        {
            if (ModelState.IsValid)
            {
                //GET

                var user = await _userManager.GetUserAsync(User);
                nec.utitlisateur = user;
                


                //SET
                _db.Add(nec);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");

            }return View(nec);

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
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var getUserId = await _db.Reservations.FindAsync(id);
            return View(getUserId);
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
