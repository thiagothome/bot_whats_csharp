using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using whats_csharp.Data;
using whats_csharp.Models;
using Microsoft.AspNetCore.Authorization;

namespace whats_csharp.Controllers
{
    [Authorize]
    public class RedefinirSenhaController : Controller
    {
        private readonly Contexto _contexto;

        public RedefinirSenhaController(Contexto contexto)
        {
            _contexto = contexto;
        }

        public IActionResult RedefinirSenha()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmarRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            redefinirSenhaModel.Email = TempData["Email"]?.ToString() ?? string.Empty;
            ModelState.Clear();

            var usuarios = _contexto.Usuarios.FirstOrDefault(u => u.Email == redefinirSenhaModel.Email);

            if (!ModelState.IsValid)
            {
                return View("RedefinirSenha", redefinirSenhaModel);
            }

            if (usuarios != null)
            {
                usuarios.Senha = redefinirSenhaModel.Senha;
                await _contexto.SaveChangesAsync();
            }

            return RedirectToAction("Login", "Login");
        }
    }
}