using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace whats_csharp.Models
{
  public class UsuarioModel
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
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
        ErrorMessage = "A senha deve conter pelo menos uma letra maiúscula, uma minúscula, um número e um caractere especial.")]
    public required string Senha { get; set; }

    [NotMapped]
    [Compare("Senha", ErrorMessage = "As senhas não coincidem.")]
    [DataType(DataType.Password)]
    [Column("confirma_senha")]
    public string? ConfirmaSenha { get; set; }

    [Column("tentativas_falhas")]
    public int TentativasFalhas { get; set; } = 0;

    [Column("ultima_tentativa")]
    public DateTime? UltimaTentativa { get; set; }

    [Column("bloqueado_ate")]
    public DateTime? BloqueadoAte { get; set; }
  }
}
