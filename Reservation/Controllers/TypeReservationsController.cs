﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Reservation.Data;

namespace Reservation.Controllers
{
    public class TypeReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypeReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TypeReservations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Types.ToListAsync());
        }

        // GET: TypeReservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeReservation = await _context.Types
                .FirstOrDefaultAsync(m => m.id_type == id);
            if (typeReservation == null)
            {
                return NotFound();
            }

            return View(typeReservation);
        }

        // GET: TypeReservations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeReservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_type,Nom,NombreApp")] TypeReservation typeReservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeReservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeReservation);
        }

        // GET: TypeReservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeReservation = await _context.Types.FindAsync(id);
            if (typeReservation == null)
            {
                return NotFound();
            }
            return View(typeReservation);
        }

        // POST: TypeReservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_type,Nom,NombreApp")] TypeReservation typeReservation)
        {
            if (id != typeReservation.id_type)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeReservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeReservationExists(typeReservation.id_type))
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
            return View(typeReservation);
        }

        // GET: TypeReservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeReservation = await _context.Types
                .FirstOrDefaultAsync(m => m.id_type == id);
            if (typeReservation == null)
            {
                return NotFound();
            }

            return View(typeReservation);
        }

        // POST: TypeReservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeReservation = await _context.Types.FindAsync(id);
            _context.Types.Remove(typeReservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeReservationExists(int id)
        {
            return _context.Types.Any(e => e.id_type == id);
        }
    }
}
