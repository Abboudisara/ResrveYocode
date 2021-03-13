using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Reserve.Data;

namespace Reserve.Controllers
{
    public class reservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public reservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: reservations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Reservations.Include(r => r.typeReservation).Include(r => r.user);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.typeReservation)
                .Include(r => r.user)
                .FirstOrDefaultAsync(m => m.id_reservation == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: reservations/Create
        public IActionResult Create()
        {
            ViewData["typeReservationid_type"] = new SelectList(_context.Types, "id_type", "Nom");
            ViewData["User_id"] = new SelectList(_context.user, "Id", "Id");
            return View();
        }

        // POST: reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_reservation,date,typeReservationid_type,User_id")] reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["typeReservationid_type"] = new SelectList(_context.Types, "id_type", "Nom", reservation.typeReservationid_type);
            ViewData["User_id"] = new SelectList(_context.user, "Id", "Id", reservation.User_id);
            return View(reservation);
        }

        // GET: reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["typeReservationid_type"] = new SelectList(_context.Types, "id_type", "Nom", reservation.typeReservationid_type);
            ViewData["User_id"] = new SelectList(_context.user, "Id", "Id", reservation.User_id);
            return View(reservation);
        }

        // POST: reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_reservation,date,typeReservationid_type,User_id")] reservation reservation)
        {
            if (id != reservation.id_reservation)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!reservationExists(reservation.id_reservation))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["typeReservationid_type"] = new SelectList(_context.Types, "id_type", "Nom", reservation.typeReservationid_type);
            ViewData["User_id"] = new SelectList(_context.user, "Id", "Id", reservation.User_id);
            return View(reservation);
        }

        // GET: reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.typeReservation)
                .Include(r => r.user)
                .FirstOrDefaultAsync(m => m.id_reservation == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool reservationExists(int id)
        {
            return _context.Reservations.Any(e => e.id_reservation == id);
        }
    }
}
