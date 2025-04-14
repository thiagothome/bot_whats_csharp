using Microsoft.AspNetCore.Mvc;
using whats_csharp.Services;
using whats_csharp.Data;
using Microsoft.EntityFrameworkCore;
using whats_csharp.Models;
using Microsoft.AspNetCore.Authorization;

namespace whats_csharp.Controllers
{
    [Authorize]
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

        public async Task<IActionResult> EnviarCodigo(EsqueciSenhaModel esquecisenhamodel)
        {
            var usuario = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Email == esquecisenhamodel.Email);
            if (usuario == null)
            {
                ModelState.AddModelError(string.Empty, "E-mail não encontrado.");
                return View("EsqueciSenha");
            }
            ;

            string codigo = new Random().Next(100000, 999999).ToString();

            _contexto.Confirmacoes.Add(new CodigoConfirmacaoModel
            {
                Email = esquecisenhamodel.Email,
                CodigoRecuperacao = codigo,
                TempoExpiracaCodigo = DateTime.Now.AddMinutes(1)
            });

            await _contexto.SaveChangesAsync();
            await _serviceEmail.EnviarCodigo(usuario.Email, codigo);

            TempData["Email"] = esquecisenhamodel.Email;

            return RedirectToAction("VerificaCodigo", "VerificaCodigo");
        }
    }
}