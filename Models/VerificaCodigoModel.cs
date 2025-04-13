using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Antiforgery;

namespace whats_csharp.Models
{
  public class VerificaCodigoModel
  {
    [Required(ErrorMessage = "O código é obrigatório.")]
    [Column("codigo_verificacao")]
    public required string CodigoVerificacao { get; set; }
  }
}