using Microsoft.EntityFrameworkCore;
using whats_csharp.Models;

namespace whats_csharp.Data
{
    public class Contexto : DbContext
    {
        public DbSet<UsuarioModel> Usuarios { get; set; }

        public Contexto(DbContextOptions<Contexto> options)
            : base(options)
        {
        }
    }
}
