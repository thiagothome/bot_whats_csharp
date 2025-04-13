using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace whats_csharp.Models
{
    public class RedefinirSenhaModel
    {
        public string Email { get; set; }
        [Required(ErrorMessage = "A senha é obrigatória.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; } = string.Empty;

        [NotMapped]
        [Required(ErrorMessage = "A confirmação de senha é obrigatória.")]
        [Compare("Senha", ErrorMessage = "As senhas não coincidem.")]
        [DataType(DataType.Password)]
        public string? ConfirmaSenha { get; set; }

    }
}