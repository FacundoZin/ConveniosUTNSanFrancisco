using APIconvenios.Common.Enums;
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
        public DbSet<Area> Carreras { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 👉 Índice único para Usuario.Username
            modelBuilder.Entity<Usuario>().HasIndex(u => u.Username).IsUnique();

            // 👉 Seed fijo de Carreras (esto sí queda)
            modelBuilder.Entity<Area>().HasData(
                new Area { Id = 1, Nombre = "Ingeniería Química" },
                new Area { Id = 2, Nombre = "Ingeniería en Sistemas" },
                new Area { Id = 3, Nombre = "Ingeniería Electrónica" },
                new Area { Id = 4, Nombre = "Ingeniería Electromecánica" },
                new Area { Id = 5, Nombre = "Tecnicatura en Programación" },
                new Area { Id = 6, Nombre = "Materias Basicas" },
                new Area { Id = 7, Nombre = "SEU" },
                new Area { Id = 8, Nombre = "Vinculación Tecnológica" }
            );

            // 👉 Many-to-Many: Carreras <-> ConvenioEspecifico (sin seed)
            modelBuilder.Entity<ConvenioEspecifico>()
                .HasMany(c => c.CarrerasInvolucradas)
                .WithMany(c => c.ConveniosInvolucrados)
                .UsingEntity<Dictionary<string, object>>(
                    "CarrerasConvenioEspecifico",
                    j => j
                        .HasOne<Area>()
                        .WithMany()
                        .HasForeignKey("CarreraId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<ConvenioEspecifico>()
                        .WithMany()
                        .HasForeignKey("ConvenioEspecificoId")
                        .OnDelete(DeleteBehavior.Cascade)
                );

            // 👉 Many-to-Many: Involucrados <-> ConvenioEspecifico (sin seed)
            modelBuilder.Entity<ConvenioEspecifico>()
                .HasMany(c => c.Involucrados)
                .WithMany(i => i.ConveniosEspecificos)
                .UsingEntity<Dictionary<string, object>>(
                    "InvolucradosConvenioEspecifico",
                    j => j
                        .HasOne<Involucrados>()
                        .WithMany()
                        .HasForeignKey("InvolucradosId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<ConvenioEspecifico>()
                        .WithMany()
                        .HasForeignKey("ConvenioEspecificoId")
                        .OnDelete(DeleteBehavior.Cascade)
                );

            // 👉 Relación Involucrados -> Carrera
            modelBuilder.Entity<Involucrados>()
                .HasOne(i => i.Carrera)
                .WithMany()
                .HasForeignKey(i => i.IdCarrera)
                .IsRequired(false);

            // 👉 Relación Empresa -> ConvenioMarco (1:1)
            modelBuilder.Entity<Empresa>()
                .HasOne(e => e.ConvenioMarco)
                .WithOne(cm => cm.Empresa)
                .HasForeignKey<ConvenioMarco>(cm => cm.EmpresaId)
                .IsRequired(false);
        }
    }
}
