using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using whats_csharp.Data;
using whats_csharp.Models;
using Microsoft.AspNetCore.Authorization;

namespace whats_csharp.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {

        private readonly Contexto _contexto;

        public LoginController(Contexto contexto)
        {
            _contexto = contexto;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logar(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", loginModel);
            }

            var usuario = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Email == loginModel.Email);

            if (usuario == null)
            {
                ModelState.AddModelError(string.Empty, "E-mail ou senha inválidos.");
                return View("Login", loginModel);
            }

            if (usuario.UltimaTentativa != null && (DateTime.Now - usuario.UltimaTentativa.Value).TotalMinutes > 30)
            {
                usuario.TentativasFalhas = 0;
            }

            if (usuario.BloqueadoAte != null && usuario.BloqueadoAte > DateTime.Now)
            {
                var tempoParaDesbloqueio = usuario.BloqueadoAte - DateTime.Now;
                ModelState.AddModelError(string.Empty, $"Conta bloqueada. Tente daqui 15 minutos.");
                return View("Login", loginModel);
            }

            if (usuario.Senha != loginModel.Senha)
            {
                usuario.TentativasFalhas++;
                usuario.UltimaTentativa = DateTime.Now;
                if (usuario.TentativasFalhas > 3)
                {
                    usuario.BloqueadoAte = DateTime.Now.AddMinutes(15);
                    usuario.TentativasFalhas = 0;
                }

                await _contexto.SaveChangesAsync();
                ModelState.AddModelError(string.Empty, "E-mail ou senha inválidos.");
                return View("Login", loginModel);
            }

            usuario.BloqueadoAte = null;
            usuario.TentativasFalhas = 0;
            await _contexto.SaveChangesAsync();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
            };

            var identidade = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identidade);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Mensagem", "Mensagem");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Login");
        }
    }
}