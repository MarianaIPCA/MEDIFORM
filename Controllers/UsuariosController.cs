using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGH2425_V3.Data;
using SGH2425_V3.Models;
using System.Security.Claims;

namespace SGH2425_V3.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UserManager<UtilizadorAplicacao> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<UtilizadorAplicacao> _signInManager;
        private readonly ApplicationDbContext _context;
        public UsuariosController(ApplicationDbContext context, UserManager<UtilizadorAplicacao> userManager, RoleManager<IdentityRole> roleManager, SignInManager<UtilizadorAplicacao> signInManager)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;


        }

        // GET: Usuarios
        public async Task <ActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Create(UtilizadorAplicacao utilizador)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                UtilizadorAplicacao registrarUtilizador = new();
                registrarUtilizador.Name = utilizador.Name;
                registrarUtilizador.UserName = utilizador.UserName;
                registrarUtilizador.NivelAcesso = utilizador.NivelAcesso;
                registrarUtilizador.NormalizedUserName = utilizador.UserName;
                registrarUtilizador.Email = utilizador.Email;
                registrarUtilizador.EmailConfirmed = true;
                registrarUtilizador.PhoneNumber = utilizador.PhoneNumber;

                var resultado = await _userManager.CreateAsync(registrarUtilizador, utilizador.PasswordHash);

                if (resultado.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View();
            } 
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuarios/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
