using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SGH2425_V3.Data;
using SGH2425_V3.Models;

namespace SGH2425_V3.Controllers
{
    public class UtentesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UtentesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Utentes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Utente.Include(u => u.AlergiaUtente);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Utentes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utente = await _context.Utente
                .Include(u => u.AlergiaUtente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utente == null)
            {
                return NotFound();
            }

            return View(utente);
        }

        // GET: Utentes/Create
        public IActionResult Create()
        {
            ViewData["AlergiaUtenteId"] = new SelectList(_context.AlergiaUtente, "Id", "Tipo_Alergia");
            return View();
        }

        // POST: Utentes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Utente utente)
        {
           
                _context.Add(utente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
         
            ViewData["AlergiaUtenteId"] = new SelectList(_context.AlergiaUtente, "Id", "Tipo_Alergia", utente.AlergiaUtenteId);
            return View(utente);
        }

        // GET: Utentes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utente = await _context.Utente.FindAsync(id);
            if (utente == null)
            {
                return NotFound();
            }
            ViewData["AlergiaUtenteId"] = new SelectList(_context.AlergiaUtente, "Id", "Tipo_Alergia", utente.AlergiaUtenteId);
            return View(utente);
        }

        // POST: Utentes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Utente utente)
        {
            if (id != utente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtenteExists(utente.Id))
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
            ViewData["AlergiaUtenteId"] = new SelectList(_context.AlergiaUtente, "Id", "Tipo_Alergia", utente.AlergiaUtenteId);
            return View(utente);
        }

        // GET: Utentes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utente = await _context.Utente
                .Include(u => u.AlergiaUtente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utente == null)
            {
                return NotFound();
            }

            return View(utente);
        }

        // POST: Utentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var utente = await _context.Utente.FindAsync(id);
            if (utente != null)
            {
                _context.Utente.Remove(utente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtenteExists(int id)
        {
            return _context.Utente.Any(e => e.Id == id);
        }
    }
}
