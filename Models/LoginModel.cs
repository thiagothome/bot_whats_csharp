using System.ComponentModel.DataAnnotations;

namespace whats_csharp.Models
{
    public class LoginModel
    {

        [Required(ErrorMessage = "E-mail obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public  required string Email { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "A senha é obrigatória.")]
        [DataType(DataType.Password)]
        public  required string Senha { get; set; } = string.Empty;
    }
}