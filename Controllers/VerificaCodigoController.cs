using Microsoft.AspNetCore.Mvc;

namespace whats_csharp.Controllers
{


    public class VerificaCodigoController : Controller
    {
        public IActionResult VerificaCodigo()
        {
            return View();
        }

        public IActionResult VerificarCodigo()
        {
            return View("RedefinirSenha");
        }
    }
}