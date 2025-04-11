using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using whats_csharp.Data;

namespace whats_csharp.Controllers
{
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

        public IActionResult ConfirmarRedefinirSenha()
        {
            return RedirectToAction("Login", "Login");
        }
    }
}