using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using whats_csharp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Runtime.InteropServices;

namespace whats_csharp.Controllers
{
    public class MensagemController : Controller
    {
        public IActionResult Mensagem()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Enviar([FromBody] MensagemModel dados)
        {
            if (dados?.Telefones == null || string.IsNullOrWhiteSpace(dados.Mensagem))
            {
                return BadRequest("Telefones ou mensagem inválidos.");
            }

            var chromeOptions = new ChromeOptions();
            string userDataDir = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? @"C:\Temp\PerfilWhatsApp"
                : "/home/thiago/.config/PerfilWhatsApp";

            chromeOptions.AddArgument($"user-data-dir={userDataDir}");
            IWebDriver driver = new ChromeDriver(chromeOptions);

            try
            {
                driver.Navigate().GoToUrl("https://web.whatsapp.com");
                Thread.Sleep(30000);

                foreach (var telefone in dados.Telefones)
                {
                    string url = $"https://web.whatsapp.com/send?phone={telefone}&text={Uri.EscapeDataString(dados.Mensagem)}";
                    driver.Navigate().GoToUrl(url);
                    Thread.Sleep(8000);

                    try
                    {
                        var botaoEnviar = driver.FindElement(By.XPath("//span[@data-icon='send']"));
                        botaoEnviar.Click();
                        Thread.Sleep(3000);
                    }
                    catch (NoSuchElementException)
                    {
                    }
                }

                return Ok("Mensagens enviadas com sucesso.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao enviar mensagens.");
            }
            finally
            {
                driver.Quit();
            }
        }
    }
}