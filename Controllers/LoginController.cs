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

            if(!ModelState.IsValid){

                return View("Login", loginModel);
            }

            var usuario = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Email == loginModel.Email);

            if(usuario == null || usuario.Senha != loginModel.Senha){
                ModelState.AddModelError(string.Empty, "E-mail ou senha inválidos.");
                return View("Login", loginModel);
            }

            return RedirectToAction("Mensagem", "Mensagem");
        }
    }
}