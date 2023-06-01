using Microsoft.EntityFrameworkCore;

using ProjVideoClub.Models;
namespace ProjVideoClub.Data
{
    public class ApplicationDbContext :DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options )
            : base( options )   
        {
            
        }

        //aqui:
        public DbSet<Utilizador>? TUtilizadores { get; set; }
        public DbSet<RegistoAluguer>? TRegistoAlugueres { get;set; }
        public DbSet<Filme>? TFilmes { get; set; }
        public DbSet<Categoria>? TCategorias { get; set; }

    }
}

