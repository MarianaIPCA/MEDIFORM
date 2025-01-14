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
    public class Tipo_MedicamentoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Tipo_MedicamentoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tipo_Medicamento
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoMedicamento.ToListAsync());
        }

        // GET: Tipo_Medicamento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipo_Medicamento = await _context.TipoMedicamento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipo_Medicamento == null)
            {
                return NotFound();
            }

            return View(tipo_Medicamento);
        }

        // GET: Tipo_Medicamento/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tipo_Medicamento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Tipo_Medicamento tipo_Medicamento)
        {
           
                _context.Add(tipo_Medicamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
         
            return View(tipo_Medicamento);
        }

        // GET: Tipo_Medicamento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipo_Medicamento = await _context.TipoMedicamento.FindAsync(id);
            if (tipo_Medicamento == null)
            {
                return NotFound();
            }
            return View(tipo_Medicamento);
        }

        // POST: Tipo_Medicamento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tipo_Medicamento tipo_Medicamento)
        {
            if (id != tipo_Medicamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipo_Medicamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Tipo_MedicamentoExists(tipo_Medicamento.Id))
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
            return View(tipo_Medicamento);
        }

        // GET: Tipo_Medicamento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipo_Medicamento = await _context.TipoMedicamento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipo_Medicamento == null)
            {
                return NotFound();
            }

            return View(tipo_Medicamento);
        }

        // POST: Tipo_Medicamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipo_Medicamento = await _context.TipoMedicamento.FindAsync(id);
            if (tipo_Medicamento != null)
            {
                _context.TipoMedicamento.Remove(tipo_Medicamento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Tipo_MedicamentoExists(int id)
        {
            return _context.TipoMedicamento.Any(e => e.Id == id);
        }
    }
}
