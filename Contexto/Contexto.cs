using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using whats_csharp.Models;

namespace whats_csharp.Data
{
    public class Contexto : DbContext
    {
        public DbSet<CadastroModel> Usuarios { get; set; }

        public Contexto(DbContextOptions<Contexto> options)
            : base(options)
        {
        }
    }
}
