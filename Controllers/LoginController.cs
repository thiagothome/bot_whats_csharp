using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using whats_csharp.Data;
using whats_csharp.Models;

namespace whats_csharp.Controllers
{
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
            return RedirectToAction("Mensagem", "Mensagem");
        }
    }
}