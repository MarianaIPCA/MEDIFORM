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

namespace SGH2425_V3.Controllers
{
    public class SolicitarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SolicitarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Solicitars
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Solicitacao.Include(s => s.Medicamento).Include(s => s.Prescricao).Include(s => s.RegistradoPor);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> PrecricaoSolicitacao()
        {
            var precricao = _context.Solicitacao
                .Include(s => s.Medicamento).
                Include(s => s.Prescricao).
                Include(s => s.RegistradoPor)
                .ToListAsync();
                
               return View(precricao);
        }

        // GET: Solicitars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitar = await _context.Solicitacao
                .Include(s => s.Medicamento)
                .Include(s => s.Prescricao)
                .Include(s => s.RegistradoPor)
                .FirstOrDefaultAsync(m => m.PrescricaoId == id);
            if (solicitar == null)
            {
                return NotFound();
            }

            return View(solicitar);
        }

        // GET: Solicitars/Create
        public IActionResult Create()
        {
            ViewData["MedicamentoId"] = new SelectList(_context.Medicamento, "Id", "Nome_medicamento");
            ViewData["PrescricaoId"] = new SelectList(_context.Prescricoes, "Id", "CodigoPrescricao");
            ViewData["RegistradoPorId"] = new SelectList(_context.Conta, "Id", "Name");
            return View();
        }

        // POST: Solicitars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Solicitar solicitar)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            solicitar.RegistradoPorId = userId;

            _context.Add(solicitar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
           
            ViewData["MedicamentoId"] = new SelectList(_context.Medicamento, "Id", "Nome_medicamento", solicitar.MedicamentoId);
            ViewData["PrescricaoId"] = new SelectList(_context.Prescricoes, "Id", "CodigoPrescricao", solicitar.PrescricaoId);
            ViewData["RegistradoPorId"] = new SelectList(_context.Conta, "Id", "Name", solicitar.RegistradoPorId);
            return View(solicitar);
        }

        // GET: Solicitars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitar = await _context.Solicitacao.FindAsync(id);
            if (solicitar == null)
            {
                return NotFound();
            }
            ViewData["MedicamentoId"] = new SelectList(_context.Medicamento, "Id", "Nome_medicamento", solicitar.MedicamentoId);
            ViewData["PrescricaoId"] = new SelectList(_context.Prescricoes, "Id", "CodigoPrescricao", solicitar.PrescricaoId);
            ViewData["RegistradoPorId"] = new SelectList(_context.Conta, "Id", "Name", solicitar.RegistradoPorId);
            return View(solicitar);
        }

        // POST: Solicitars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Solicitar solicitar)
        {
            if (id != solicitar.PrescricaoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solicitar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolicitarExists(solicitar.PrescricaoId))
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
            ViewData["MedicamentoId"] = new SelectList(_context.Medicamento, "Id", "Nome_medicamento", solicitar.MedicamentoId);
            ViewData["PrescricaoId"] = new SelectList(_context.Prescricoes, "Id", "CodigoPrescricao", solicitar.PrescricaoId);
            ViewData["RegistradoPorId"] = new SelectList(_context.Conta, "Id", "Name", solicitar.RegistradoPorId);
            return View(solicitar);
        }

        // GET: Solicitars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitar = await _context.Solicitacao
                .Include(s => s.Medicamento)
                .Include(s => s.Prescricao)
                .Include(s => s.RegistradoPor)
                .FirstOrDefaultAsync(m => m.PrescricaoId == id);
            if (solicitar == null)
            {
                return NotFound();
            }

            return View(solicitar);
        }

        // POST: Solicitars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var solicitar = await _context.Solicitacao.FindAsync(id);
            if (solicitar != null)
            {
                _context.Solicitacao.Remove(solicitar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolicitarExists(int id)
        {
            return _context.Solicitacao.Any(e => e.PrescricaoId == id);
        }
    }
}
