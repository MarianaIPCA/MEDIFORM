using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SGH2425_V3.Data;
using SGH2425_V3.Models;
using SGH2425_V3.Controllers;
using SGH2425_V3.Data.Migrations;
using System.Security.Claims;


namespace SGH2425_V3.Controllers
{
    public class MovimentoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovimentoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Movimentoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Movimento.Include(m => m.RegistradoPor).Include(m => m.Solicitar);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Movimentoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimento = await _context.Movimento
                .Include(m => m.RegistradoPor)
                .Include(m => m.Solicitar)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movimento == null)
            {
                return NotFound();
            }

            return View(movimento);
        }

        // GET: Movimentoes/Create
        public IActionResult Create()
        {
            ViewData["RegistradoPorId"] = new SelectList(_context.Conta, "Id", "Name");
            ViewData["PrescricaoId"] = new SelectList(_context.Solicitacao.Select(s => new {s.PrescricaoId,s.Prescricao.CodigoPrescricao}).Distinct(), "PrescricaoId", "CodigoPrescricao");
            ViewData["MedicamentoId"] = new SelectList(_context.Solicitacao.Select(s => new { s.MedicamentoId, s.Medicamento.Nome_medicamento }).Distinct(), "MedicamentoId", "Nome_medicamento");
            return View();
        }

        // POST: Movimentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Movimento movimento)
        {
            //Movimento movitacacao = new Movimento();

         
           
            try
            {
              
                MovimentacaoEViewModel movitacaoViewModel = new MovimentacaoEViewModel();
                // Localiza o medicamento associado ao movimento
                var medicamento = await _context.Medicamento.FindAsync(movimento.MedicamentoId);
                if (medicamento == null)
                {
                    ModelState.AddModelError("", "Medicamento não encontrado.");
                    return View(movimento);
                }

                // Atualiza o estoque com base no tipo de movimento (entrada ou saída)
                if (movimento.Tipo_Movimento == "Saida") // Verifica se é uma saída
                {
                    if (medicamento.stock < movimento.Qtd_Movimento)
                    {
                        ModelState.AddModelError("", "Estoque insuficiente para realizar a saída.");
                        return View(movimento);
                    }

                    medicamento.stock -= movimento.Qtd_Movimento; // Reduz o estoque

                }
                else if (movimento.Tipo_Movimento == "Entrada") // Verifica se é uma entrada
                {
                    medicamento.stock += movimento.Qtd_Movimento; // Aumenta o estoque
                }
                else
                {
                    ModelState.AddModelError("", "Tipo de movimento inválido.");
                    return View(movimento);
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                movimento.DataMovimento = DateTime.Now;
                movimento.RegistradoPorId = userId;

                // Adiciona o movimento ao banco de dados
                _context.Add(movimento);

                // Atualiza o estoque do medicamento no banco
                _context.Update(medicamento);

                // Salva as alterações no banco de dados
                await _context.SaveChangesAsync();



                return RedirectToAction(nameof(Index));


                ViewData["RegistradoPorId"] = new SelectList(_context.Conta, "Id", "Name", movimento.RegistradoPorId);
                ViewData["PrescricaoId"] = new SelectList(_context.Solicitacao, "PrescricaoId", "CodigoPrescricao", movimento.PrescricaoId);
                ViewData["MedicamentoId"] = new SelectList(_context.Solicitacao, "MedicamentoId", "Nome_medicamento", movimento.MedicamentoId);
               
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Não existe Prescrição Para este Utente.");
            }

            return View(movimento);

        }

        // GET: Movimentoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimento = await _context.Movimento.FindAsync(id);
            if (movimento == null)
            {
                return NotFound();
            }
            ViewData["RegistradoPorId"] = new SelectList(_context.Conta, "Id", "Name", movimento.RegistradoPorId);
            ViewData["PrescricaoId"] = new SelectList(_context.Solicitacao, "PrescricaoId", "CodigoPrescricao", movimento.PrescricaoId);
            ViewData["MedicamentoId"] = new SelectList(_context.Solicitacao, "MedicamentoId", "Nome_medicamento", movimento.MedicamentoId);
            return View();
        }

        // POST: Movimentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Movimento movimento)
        {
            if (id != movimento.Id)
            {
                return NotFound();
            }

       
                try
                {
                    _context.Update(movimento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovimentoExists(movimento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
          
            ViewData["RegistradoPorId"] = new SelectList(_context.Conta, "Id", "Name", movimento.RegistradoPorId);
            ViewData["PrescricaoId"] = new SelectList(_context.Solicitacao, "PrescricaoId", "CodigoPrescricao", movimento.PrescricaoId);
            ViewData["MedicamentoId"] = new SelectList(_context.Solicitacao, "MedicamentoId", "Nome_medicamento", movimento.MedicamentoId);
            return View(movimento);
        }

        // GET: Movimentoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimento = await _context.Movimento
                .Include(m => m.RegistradoPor)
                .Include(m => m.Solicitar)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movimento == null)
            {
                return NotFound();
            }

            return View(movimento);
        }

        // POST: Movimentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movimento = await _context.Movimento.FindAsync(id);
            if (movimento != null)
            {
                _context.Movimento.Remove(movimento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovimentoExists(int id)
        {
            return _context.Movimento.Any(e => e.Id == id);
        }
    }
}
