using APIconvenios.Models;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Data
{
    public class ApplicationDbContext : DbContext    
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<ConvenioMarco> ConveniosMarcos { get; set; }  
        public DbSet<ConvenioEspecifico> ConveniosEspecificos { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Involucrados> Involucrados { get; set; }

    }
}
