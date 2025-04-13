using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace whats_csharp.Models
{
    public class CodigoConfirmacaoModel
    {
        public int Id { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "Código inválido.")]
        public string CodigoRecuperacao { get; set; }
        public DateTime TempoExpiracaCodigo { get; set; }
    }
}