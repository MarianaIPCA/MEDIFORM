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
    public class AlergiaUtentesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlergiaUtentesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AlergiaUtentes
        public async Task<IActionResult> Index()
        {
            return View(await _context.AlergiaUtente.ToListAsync());
        }

        // GET: AlergiaUtentes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alergiaUtente = await _context.AlergiaUtente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alergiaUtente == null)
            {
                return NotFound();
            }

            return View(alergiaUtente);
        }

        // GET: AlergiaUtentes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AlergiaUtentes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tipo_Alergia,Descricao,Data_Ultima_Alergia")] AlergiaUtente alergiaUtente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alergiaUtente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alergiaUtente);
        }

        // GET: AlergiaUtentes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alergiaUtente = await _context.AlergiaUtente.FindAsync(id);
            if (alergiaUtente == null)
            {
                return NotFound();
            }
            return View(alergiaUtente);
        }

        // POST: AlergiaUtentes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tipo_Alergia,Descricao,Data_Ultima_Alergia")] AlergiaUtente alergiaUtente)
        {
            if (id != alergiaUtente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alergiaUtente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlergiaUtenteExists(alergiaUtente.Id))
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
            return View(alergiaUtente);
        }

        // GET: AlergiaUtentes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alergiaUtente = await _context.AlergiaUtente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alergiaUtente == null)
            {
                return NotFound();
            }

            return View(alergiaUtente);
        }

        // POST: AlergiaUtentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alergiaUtente = await _context.AlergiaUtente.FindAsync(id);
            if (alergiaUtente != null)
            {
                _context.AlergiaUtente.Remove(alergiaUtente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlergiaUtenteExists(int id)
        {
            return _context.AlergiaUtente.Any(e => e.Id == id);
        }
    }
}
