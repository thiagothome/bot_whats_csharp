using Microsoft.AspNetCore.Mvc;
using whats_csharp.Data;
using whats_csharp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace whats_csharp.Controllers
{
    [Authorize]
    public class VerificaCodigoController : Controller
    {
        private readonly Contexto _contexto;

        public VerificaCodigoController(Contexto contexto)
        {
            _contexto = contexto;
        }

        public IActionResult VerificaCodigo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerificarCodigo(VerificaCodigoModel verificaCodigoModel)
        {
            var email = TempData["Email"]?.ToString() ?? string.Empty;


            if (!ModelState.IsValid)
            {
                return View("VerificaCodigo", verificaCodigoModel);
            }

            var confirmacoes = await _contexto.Confirmacoes
            .Where(c => c.Email == email && c.CodigoRecuperacao == verificaCodigoModel.CodigoVerificacao)
            .OrderByDescending(c => c.TempoExpiracaCodigo)
            .FirstOrDefaultAsync();

            if (confirmacoes == null)
            {
                ModelState.AddModelError("", "Código inválido.");
                return View("VerificaCodigo", verificaCodigoModel);
            }

            if (DateTime.Now > confirmacoes.TempoExpiracaCodigo)
            {
                TempData["Erro"] = "O código expirou. Por favor, solicite um novo.";
                return RedirectToAction("EsqueciSenha", "EsqueciSenha");
            }

            TempData["Email"] = confirmacoes.Email;

            return RedirectToAction("RedefinirSenha", "RedefinirSenha");
        }
    }
}