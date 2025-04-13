using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Antiforgery;

namespace whats_csharp.Models
{
    public class CodigoConfirmacaoModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("email")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Código inválido.")]

        [Column("codigo_recuperacao")]
        public required string CodigoRecuperacao { get; set; }

        [Column("tempo_expiracao_codigo")]
        public DateTime TempoExpiracaCodigo { get; set; }
    }
}