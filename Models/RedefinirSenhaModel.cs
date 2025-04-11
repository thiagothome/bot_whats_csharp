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
        [Required(ErrorMessage = "A senha é obrigatória.")]
        [DataType(DataType.Password)]
        public required string Senha { get; set; }

        [NotMapped]
        [Compare("Senha", ErrorMessage = "As senhas não coincidem.")]
        public string? ConfirmaSenha { get; set; }
    }
}