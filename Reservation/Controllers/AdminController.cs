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
     

        public AdminController(ApplicationDbContext db )
        {
            _db = db;

        }
       
        public IActionResult Index()
        {


            var List = _db.Reservations.Include(c => c.utitlisateur).OrderBy(c => c.utitlisateur.Counter) ;
            
            return View(List);
        }

      
        [HttpGet]
        public async Task<IActionResult> Index(String ResrveSearch)
        {
            ViewData["GetReserve"] = ResrveSearch;
            var Reseveq = from x in _db.Reservations.Include(c => c.utitlisateur).OrderBy(c => c.utitlisateur.Counter) select x;
            if (!string.IsNullOrEmpty(ResrveSearch))
            {
                Reseveq = Reseveq.Where(x => x.date.Contains(ResrveSearch) || x.Status.Contains(ResrveSearch) || x.utitlisateur.Email.Contains(ResrveSearch)) ;
            }
            return View(await Reseveq.AsNoTracking().ToListAsync());
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



        // Counter:
        public IActionResult Status(int id)
        {
           
            var reservation = _db.Reservations.Include(x => x.utitlisateur).FirstOrDefault(x => x.id_reservation == id);
            reservation.Status="Approve";
            reservation.utitlisateur.Counter++;
            _db.Reservations.Update(reservation);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

    }


}


