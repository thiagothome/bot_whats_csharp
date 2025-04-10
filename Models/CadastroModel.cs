using System.ComponentModel.DataAnnotations;

namespace whats_csharp.Models
{
  public class CadastroModel
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    public required string Nome { get; set; }

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
    [DataType(DataType.Password)]
    public required string Senha { get; set; }

    [Required(ErrorMessage = "A confirmação da senha é obrigatória.")]
    [Compare("Senha", ErrorMessage = "As senhas não coincidem.")]
    [DataType(DataType.Password)]
    public required string ConfirmaSenha { get; set; }

    public string? CodigoRecuperacao { get; set; }
  }
}
