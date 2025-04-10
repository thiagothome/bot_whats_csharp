namespace whats_csharp.Services
{
    public interface IEmailService
    {
        Task EnviarCodigo(string emailDestino, string codigoRecuperacao);
    }
}
