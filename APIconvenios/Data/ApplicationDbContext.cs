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
        public DbSet<ArchivosAdjuntos> ArchivosAdjuntos { get; set; }
        public DbSet<Carreras> Carreras { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Carreras>().HasData(
                new Carreras { Id = 1, Nombre = "Ingeniería Química" },
                new Carreras { Id = 2, Nombre = "Ingeniería en Sistemas" },
                new Carreras { Id = 3, Nombre = "Ingeniería Electrónica" },
                new Carreras { Id = 4, Nombre = "Ingeniería Electromecánica" },
                new Carreras { Id = 5, Nombre = "Tecnicatura en Programación" },
                new Carreras { Id = 6, Nombre = "Materias Basicas" },
                new Carreras { Id = 7, Nombre = "SEU" },
                new Carreras { Id = 8, Nombre = "Vinculación Tecnológica" }
                );

            modelBuilder.Entity<ConvenioEspecifico>()
               .HasMany(c => c.CarrerasInvolucradas)
               .WithMany(c => c.ConveniosInvolucrados)
               .UsingEntity<Dictionary<string, object>>(
                   "CarrerasConvenioEspecifico", // nombre de la tabla intermedia
                   j => j
                       .HasOne<Carreras>()
                       .WithMany()
                       .HasForeignKey("CarreraId")
                       .OnDelete(DeleteBehavior.Cascade), // opcional
                   j => j
                       .HasOne<ConvenioEspecifico>()
                       .WithMany()
                       .HasForeignKey("ConvenioEspecificoId")
                       .OnDelete(DeleteBehavior.Cascade) // opcional
               );

            modelBuilder.Entity<Empresa>()
                .HasOne(e => e.ConvenioMarco)
                .WithOne(cm => cm.Empresa)              
                .HasForeignKey<ConvenioMarco>(cm => cm.EmpresaId)  
                .IsRequired(false);                  

        }
    }
}
