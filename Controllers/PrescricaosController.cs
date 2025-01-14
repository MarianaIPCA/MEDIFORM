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
    public class PrescricaosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrescricaosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Prescricaos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Prescricoes.Include(p => p.RegistradoPor).Include(p => p.Utentes);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Prescricaos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescricao = await _context.Prescricoes
                .Include(p => p.RegistradoPor)
                .Include(p => p.Utentes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prescricao == null)
            {
                return NotFound();
            }

            return View(prescricao);
        }

        // GET: Prescricaos/Create
        public IActionResult Create()
        {
            ViewData["RegistradoPorId"] = new SelectList(_context.Conta, "Id", "Name");
            ViewData["UtenteId"] = new SelectList(_context.Utente, "Id", "Nome");
            return View();
        }

        // POST: Prescricaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Prescricao prescricao)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            prescricao.DataPrescricao = DateTime.Now;
            prescricao.RegistradoPorId = userId;

                _context.Add(prescricao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            ViewData["RegistradoPorId"] = new SelectList(_context.Conta, "Id", "Name", prescricao.RegistradoPorId);
            ViewData["UtenteId"] = new SelectList(_context.Utente, "Id", "Nome", prescricao.UtenteId);
            return View(prescricao);
        }

        // GET: Prescricaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescricao = await _context.Prescricoes.FindAsync(id);
            if (prescricao == null)
            {
                return NotFound();
            }
            ViewData["RegistradoPorId"] = new SelectList(_context.Conta, "Id", "Name", prescricao.RegistradoPorId);
            ViewData["UtenteId"] = new SelectList(_context.Utente, "Id", "Nome", prescricao.UtenteId);
            return View(prescricao);
        }

        // POST: Prescricaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Prescricao prescricao)
        {
            if (id != prescricao.Id)
            {
                return NotFound();
            }

           
                try
                {
                    _context.Update(prescricao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrescricaoExists(prescricao.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
         
            ViewData["RegistradoPorId"] = new SelectList(_context.Conta, "Id", "Name", prescricao.RegistradoPorId);
            ViewData["UtenteId"] = new SelectList(_context.Utente, "Id", "Nome", prescricao.UtenteId);
            return View(prescricao);
        }

        // GET: Prescricaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescricao = await _context.Prescricoes
                .Include(p => p.RegistradoPor)
                .Include(p => p.Utentes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prescricao == null)
            {
                return NotFound();
            }

            return View(prescricao);
        }

        // POST: Prescricaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prescricao = await _context.Prescricoes.FindAsync(id);
            if (prescricao != null)
            {
                _context.Prescricoes.Remove(prescricao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrescricaoExists(int id)
        {
            return _context.Prescricoes.Any(e => e.Id == id);
        }
    }
}
