using Microsoft.AspNetCore.Mvc;
using whats_csharp.Models;

namespace whats_csharp.Controllers
{

    public class CadastroController : Controller
    {

        public IActionResult Cadastro()
        {
            return View("Cadastrar");
        }

        [HttpPost]
        public IActionResult Cadastrar(CadastroModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); 
            }


            return RedirectToAction("Login", "Login");
        }
    }
}