namespace whats_csharp.Models
{
  public class CadastroModel
  {
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string ConfirmaSenha { get; set; }
    public string CodigoRecuperacao { get; set; }
  }
}