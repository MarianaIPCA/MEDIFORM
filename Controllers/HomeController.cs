using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGH2425_V3.Data;
using SGH2425_V3.Models;
using System.Diagnostics;

namespace SGH2425_V3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context )
        {
            _logger = logger;
            _context = context;
        }

        public async Task< IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return this.Redirect("~/identity/account/login");
            }
            else {
                var medicamentos = await _context.Medicamento
                .Include(t => t.RegistradoPor)
                .OrderBy(x => x.DataRegistro)
                .ToListAsync();

                return View(medicamentos);
            }
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       

    }
}
