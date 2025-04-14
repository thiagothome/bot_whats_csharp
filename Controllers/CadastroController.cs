using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using whats_csharp.Data;
using whats_csharp.Models;
using Microsoft.AspNetCore.Authorization;

namespace whats_csharp.Controllers
{
    [Authorize]
    public class CadastroController : Controller
    {

        private readonly Contexto _contexto;

        public CadastroController(Contexto contexto)
        {
            _contexto = contexto;
        }

        public IActionResult Cadastro()
        {
            return View("Cadastrar");
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(UsuarioModel dados)
        {
            if (!ModelState.IsValid)
            {
                return View(dados);
            }

            bool emailExiste = await _contexto.Usuarios.AnyAsync(x => x.Email == dados.Email);

            if (emailExiste)
            {
                ModelState.AddModelError("Email", "Email já cadastrado.");
                return View("Cadastrar", dados);
            }

            var novoUsuario = new UsuarioModel
            {
                Nome = dados.Nome,
                Email = dados.Email,
                Senha = dados.Senha
            };

            _contexto.Add(novoUsuario);
            await _contexto.SaveChangesAsync();

            return RedirectToAction("Login", "Login");
        }
    }
}