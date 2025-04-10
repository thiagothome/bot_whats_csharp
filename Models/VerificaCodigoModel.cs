namespace whats_csharp.Models
{
  public class VerificaCodigoModel
  {
    public  required string CodigoVerificacao { get; set; } = string.Empty;
    public  required string ConfirmarCodigo { get; set; } = string.Empty;
  }
}