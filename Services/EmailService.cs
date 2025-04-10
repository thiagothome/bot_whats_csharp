using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Configuration;

namespace whats_csharp.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task EnviarCodigo(string emailDestino, string codigoRecuperacao)
        {
            var mensagem = new MimeMessage();

            mensagem.From.Add(new MailboxAddress("Dispara-W", _config["EmailSettings:From"]));
            mensagem.To.Add(MailboxAddress.Parse(emailDestino));
            mensagem.Subject = "Código de recuperação";

            mensagem.Body = new TextPart("plain")
            {
                Text = $"Seu código de recuperação é: {codigoRecuperacao}"
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(
                _config["EmailSettings:SmtpServer"],
                int.Parse(_config["EmailSettings:Port"]!),
                SecureSocketOptions.SslOnConnect
            );

            await client.AuthenticateAsync(
                _config["EmailSettings:From"],
                _config["EmailSettings:Password"]
            );

            await client.SendAsync(mensagem);
            await client.DisconnectAsync(true);
        }
    }
}
