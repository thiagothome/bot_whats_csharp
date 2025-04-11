using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Antiforgery;

namespace whats_csharp.Models
{
  public class VerificaCodigoModel
  {
    [Required(ErrorMessage = "O código é obrigatório.")]
    public required string CodigoVerificacao { get; set; }
  }
}