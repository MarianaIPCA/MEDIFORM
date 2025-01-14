using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SGH2425_V3.Data;
using SGH2425_V3.Data.Migrations;
using SGH2425_V3.Models;
using Medicamento = SGH2425_V3.Models.Medicamento;

namespace SGH2425_V3.Controllers
{
    public class MedicamentoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MedicamentoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Medicamemtoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Medicamento.Include(m => m.RegistradoPor).Include(m => m.TipoMedicamento);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Medicamemtoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicamemto = await _context.Medicamento
                .Include(m => m.RegistradoPor)
                .Include(m => m.TipoMedicamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medicamemto == null)
            {
                return NotFound();
            }

            return View(medicamemto);
        }

        // GET: Medicamemtoes/Create
        public IActionResult Create()
        {
            ViewData["RegistradoPorId"] = new SelectList(_context.Conta, "Id", "Name");
            ViewData["TipoMedicamentoId"] = new SelectList(_context.TipoMedicamento, "Id", "Tipo");
            return View();
        }

        // POST: Medicamemtoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Medicamento medicamemto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            medicamemto.DataRegistro = DateTime.Now;
            medicamemto.RegistradoPorId = userId;

            _context.Add(medicamemto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
          
            ViewData["RegistradoPorId"] = new SelectList(_context.Conta, "Id", "Name", medicamemto.RegistradoPorId);
            ViewData["TipoMedicamentoId"] = new SelectList(_context.TipoMedicamento, "Id", "Tipo", medicamemto.TipoMedicamentoId);
            return View(medicamemto);
        }

        // GET: Medicamemtoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicamemto = await _context.Medicamento.FindAsync(id);
            if (medicamemto == null)
            {
                return NotFound();
            }
            ViewData["RegistradoPorId"] = new SelectList(_context.Conta, "Id", "Name", medicamemto.RegistradoPorId);
            ViewData["TipoMedicamentoId"] = new SelectList(_context.TipoMedicamento, "Id", "Tipo", medicamemto.TipoMedicamentoId);
            return View(medicamemto);
        }

        // POST: Medicamemtoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Medicamento medicamemto)
        {
            if (id != medicamemto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicamemto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicamemtoExists(medicamemto.Id))
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
            ViewData["RegistradoPorId"] = new SelectList(_context.Conta, "Id", "Name", medicamemto.RegistradoPorId);
            ViewData["TipoMedicamentoId"] = new SelectList(_context.TipoMedicamento, "Id", "Tipo", medicamemto.TipoMedicamentoId);
            return View(medicamemto);
        }

        // GET: Medicamemtoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicamemto = await _context.Medicamento
                .Include(m => m.RegistradoPor)
                .Include(m => m.TipoMedicamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medicamemto == null)
            {
                return NotFound();
            }

            return View(medicamemto);
        }

        // POST: Medicamemtoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicamemto = await _context.Medicamento.FindAsync(id);
            if (medicamemto != null)
            {
                _context.Medicamento.Remove(medicamemto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicamemtoExists(int id)
        {
            return _context.Medicamento.Any(e => e.Id == id);
        }
    }
}
