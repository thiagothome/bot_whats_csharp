using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace whats_csharp.Controllers
{

    public class CadastroController : Controller
    {

        public IActionResult Cadastro()
        {
            return View("Cadastrar");
        }

        public IActionResult Cadastrar()
        {
            return View("Login");
        }
    }
}