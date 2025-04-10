using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using whats_csharp.Services;
using whats_csharp.Services;
using whats_csharp.Data;
using Microsoft.EntityFrameworkCore;

namespace whats_csharp.Controllers
{


    public class EsqueciSenhaController : Controller
    {
        private readonly Contexto _contexto;
        private readonly IEmailService _serviceEmail;

        public EsqueciSenhaController(Contexto contexto, IEmailService serviceEmail)
        {
            _contexto = contexto;
            _serviceEmail = serviceEmail;
        }

        public IActionResult EsqueciSenha()
        {
            return View();
        }

        public async Task<IActionResult> EnviarCodigo(string emailDestino)
        {
            var usuario = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Email == emailDestino);
            if (usuario == null)
            {
                ModelState.AddModelError("EsqueciSenha", "E-mail não encontrado.");
                return View("EsqueciSenha");
            }

            string codigo = new Random().Next(100000, 999999).ToString();
            await _serviceEmail.EnviarCodigo(emailDestino, codigo);

            return RedirectToAction("Login", "Login");
        }
    }
}