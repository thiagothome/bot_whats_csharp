using System.ComponentModel.DataAnnotations;

namespace whats_csharp.Models
{
    public class EsqueciSenhaModel
    {
        [Required(ErrorMessage = "E-mail não cadastrado.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public required string Email { get; set; }
    }
}