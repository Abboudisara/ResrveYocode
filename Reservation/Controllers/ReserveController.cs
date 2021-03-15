using Microsoft.AspNetCore.Mvc;
using Reservation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.Controllers
{
    public class ReserveController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ReserveController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var display = _db.Reservations.ToList();

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
                _db.Add(nec);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");

            }return View(nec);

        }
    }
}
