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

            // Seed Empresas - Agregando más empresas
            modelBuilder.Entity<Empresa>().HasData(
                new Empresa { Id = 1, Nombre = "Tech Solutions S.A.", RazonSocial = "Tech Solutions S.A.", Cuit = "30-12345678-9", Direccion = "Av. Tecnologica 123", Telefono = "3564112233", Email = "contacto@techsolutions.com" },
                new Empresa { Id = 2, Nombre = "Innovación Digital S.R.L.", RazonSocial = "Innovación Digital S.R.L.", Cuit = "30-87654321-0", Direccion = "Calle Innovacion 456", Telefono = "3564445566", Email = "info@innovacion.com" },
                new Empresa { Id = 3, Nombre = "Electrónica Avanzada S.A.", RazonSocial = "Electrónica Avanzada S.A.", Cuit = "30-11223344-5", Direccion = "Bv. Industrial 789", Telefono = "3564223344", Email = "contacto@electronica.com" },
                new Empresa { Id = 4, Nombre = "Química Industrial S.R.L.", RazonSocial = "Química Industrial Soc. Resp. Limitada", Cuit = "30-99887766-1", Direccion = "Parque Industrial 321", Telefono = "3564556677", Email = "info@quimica.com" },
                new Empresa { Id = 5, Nombre = "Mecatrónica del Centro", RazonSocial = "Mecatrónica del Centro S.A.", Cuit = "30-55667788-2", Direccion = "Av. Libertad 555", Telefono = "3564889900", Email = "ventas@mecatronica.com" },
                new Empresa { Id = 6, Nombre = "Consultora Educativa", RazonSocial = "Consultora Educativa y Capacitación", Cuit = "30-44556677-3", Direccion = "San Martin 888", Telefono = "3564112244", Email = "contacto@consultora.edu" }
            );

            // Seed Convenios Marcos - Cubriendo todos los estados
            modelBuilder.Entity<ConvenioMarco>().HasData(
                new ConvenioMarco { Id = 1, numeroconvenio = "CM-001/2024", Titulo = "Convenio Marco con Tech Solutions", FechaFirmaConvenio = new DateOnly(2024, 1, 15), FechaFin = new DateOnly(2026, 1, 15), Estado = EstadoConvenio.Vigente, EmpresaId = 1, Refrendado = true, NumeroResolucion = "RES-2024-001", ComentarioOpcional = "Convenio vigente principal para actividades IT" },
                new ConvenioMarco { Id = 2, numeroconvenio = "CM-002/2024", Titulo = "Convenio Marco con Innovación Digital", FechaFirmaConvenio = new DateOnly(2024, 2, 20), FechaFin = new DateOnly(2025, 2, 20), Estado = EstadoConvenio.Vigente, EmpresaId = 2, Refrendado = false },
                new ConvenioMarco { Id = 3, numeroconvenio = "CM-003/2023", Titulo = "Convenio Marco Electrónica - Finalizado", FechaFirmaConvenio = new DateOnly(2023, 5, 10), FechaFin = new DateOnly(2024, 5, 10), Estado = EstadoConvenio.Finalizado, EmpresaId = 3, Refrendado = true, NumeroResolucion = "RES-2023-045", ComentarioOpcional = "Convenio finalizado exitosamente" },
                new ConvenioMarco { Id = 4, numeroconvenio = "CM-004/2024", Titulo = "Convenio Marco Química Industrial - Borrador", FechaFirmaConvenio = null, FechaFin = null, Estado = EstadoConvenio.Borrador, EmpresaId = 4, Refrendado = false, ComentarioOpcional = "En proceso de revisión legal" },
                new ConvenioMarco { Id = 5, numeroconvenio = "CM-005/2024", Titulo = "Convenio Marco Mecatrónica", FechaFirmaConvenio = new DateOnly(2024, 6, 1), FechaFin = new DateOnly(2027, 6, 1), Estado = EstadoConvenio.Vigente, EmpresaId = 5, Refrendado = true, NumeroResolucion = "RES-2024-078" }
            );

            // Seed Convenios Especificos - Diversos escenarios
            modelBuilder.Entity<ConvenioEspecifico>().HasData(
                // Convenios con Marco
                new ConvenioEspecifico { Id = 1, numeroconvenio = "CE-001/2024", TituloConvenio = "Pasantías 2024 - Sistemas y Programación", FechaFirmaConvenio = new DateOnly(2024, 3, 1), FechaInicioActividades = new DateOnly(2024, 3, 10), FechaFinConvenio = new DateOnly(2024, 12, 31), Estado = EstadoConvenio.Vigente, ConvenioMarcoId = 1, EmpresaId = 1, EsActa = false, Refrendado = true, NumeroResolucion = "RES-2024-015", ComentarioOpcional = "Pasantías rentadas para alumnos avanzados" },
                new ConvenioEspecifico { Id = 2, numeroconvenio = "CE-002/2024", TituloConvenio = "Investigación Conjunta AI", FechaFirmaConvenio = new DateOnly(2024, 4, 5), FechaInicioActividades = new DateOnly(2024, 4, 10), FechaFinConvenio = new DateOnly(2024, 10, 30), Estado = EstadoConvenio.Borrador, ConvenioMarcoId = 2, EmpresaId = 2, EsActa = true, Refrendado = false, ComentarioOpcional = "Proyecto de investigación en IA" },
                new ConvenioEspecifico { Id = 3, numeroconvenio = "CE-003/2024", TituloConvenio = "Desarrollo de Prototipos Electrónicos", FechaFirmaConvenio = new DateOnly(2024, 3, 20), FechaInicioActividades = new DateOnly(2024, 4, 1), FechaFinConvenio = new DateOnly(2024, 11, 30), Estado = EstadoConvenio.Vigente, ConvenioMarcoId = 3, EmpresaId = 3, EsActa = false, Refrendado = true, NumeroResolucion = "RES-2024-022" },
                
                // Convenios SIN Marco (directos con empresa)
                new ConvenioEspecifico { Id = 4, numeroconvenio = "CE-004/2024", TituloConvenio = "Capacitación SEU - Seguridad Laboral", FechaFirmaConvenio = new DateOnly(2024, 5, 15), FechaInicioActividades = new DateOnly(2024, 5, 20), FechaFinConvenio = new DateOnly(2024, 7, 15), Estado = EstadoConvenio.Vigente, ConvenioMarcoId = null, EmpresaId = 6, EsActa = false, Refrendado = true, NumeroResolucion = "RES-2024-035" },
                new ConvenioEspecifico { Id = 5, numeroconvenio = "CE-005/2024", TituloConvenio = "Prácticas Profesionales Química", FechaFirmaConvenio = new DateOnly(2024, 2, 10), FechaInicioActividades = new DateOnly(2024, 2, 15), FechaFinConvenio = new DateOnly(2024, 8, 15), Estado = EstadoConvenio.Vigente, ConvenioMarcoId = null, EmpresaId = 4, EsActa = false, Refrendado = true, NumeroResolucion = "RES-2024-008" },
                
                // Convenios finalizados
                new ConvenioEspecifico { Id = 6, numeroconvenio = "CE-006/2023", TituloConvenio = "Proyecto Final Integrador 2023", FechaFirmaConvenio = new DateOnly(2023, 8, 1), FechaInicioActividades = new DateOnly(2023, 8, 10), FechaFinConvenio = new DateOnly(2023, 12, 20), Estado = EstadoConvenio.Finalizado, ConvenioMarcoId = 3, EmpresaId = 3, EsActa = false, Refrendado = true, NumeroResolucion = "RES-2023-089", ComentarioOpcional = "Proyecto finalizado con éxito" },
                
                // Actas
                new ConvenioEspecifico { Id = 7, numeroconvenio = "ACTA-001/2024", TituloConvenio = "Acta Acuerdo Vinculación Tecnológica", FechaFirmaConvenio = new DateOnly(2024, 7, 1), FechaInicioActividades = new DateOnly(2024, 7, 5), FechaFinConvenio = new DateOnly(2025, 7, 1), Estado = EstadoConvenio.Vigente, ConvenioMarcoId = 1, EmpresaId = 1, EsActa = true, Refrendado = true, NumeroResolucion = "RES-2024-055" },
                new ConvenioEspecifico { Id = 8, numeroconvenio = "ACTA-002/2024", TituloConvenio = "Acta Donación Equipamiento", FechaFirmaConvenio = new DateOnly(2024, 6, 10), FechaInicioActividades = new DateOnly(2024, 6, 15), FechaFinConvenio = new DateOnly(2024, 12, 31), Estado = EstadoConvenio.Vigente, ConvenioMarcoId = 5, EmpresaId = 5, EsActa = true, Refrendado = false },
                
                // Borrador sin fechas
                new ConvenioEspecifico { Id = 9, numeroconvenio = "CE-007/2024", TituloConvenio = "Futuro Convenio Materias Básicas", FechaFirmaConvenio = null, FechaInicioActividades = null, FechaFinConvenio = null, Estado = EstadoConvenio.Borrador, ConvenioMarcoId = null, EmpresaId = 6, EsActa = false, Refrendado = false, ComentarioOpcional = "En negociación" }
            );

            // Seed Involucrados - Cubriendo todos los roles
            modelBuilder.Entity<Involucrados>().HasData(
                // Alumnos
                new Involucrados { Id = 1, Nombre = "Juan", Apellido = "Perez", Email = "juan.perez@email.com", Telefono = "3564998877", Legajo = 12345, RolInvolucrado = Roles.Alumno, IdCarrera = 2 }, // Ing. Sistemas
                new Involucrados { Id = 2, Nombre = "Sofia", Apellido = "Martinez", Email = "sofia.martinez@email.com", Telefono = "3564887766", Legajo = 12346, RolInvolucrado = Roles.Alumno, IdCarrera = 5 }, // Tec. Programación
                new Involucrados { Id = 3, Nombre = "Lucas", Apellido = "Rodriguez", Email = "lucas.rodriguez@email.com", Telefono = "3564776655", Legajo = 12347, RolInvolucrado = Roles.Alumno, IdCarrera = 3 }, // Ing. Electrónica
                
                // Docentes
                new Involucrados { Id = 4, Nombre = "Maria", Apellido = "Gomez", Email = "maria.gomez@email.com", Telefono = "3564776655", Legajo = 67890, RolInvolucrado = Roles.Docente, IdCarrera = 2 }, // Ing. Sistemas
                new Involucrados { Id = 5, Nombre = "Carlos", Apellido = "Fernandez", Email = "carlos.fernandez@utn.edu.ar", Telefono = "3564665544", Legajo = 67891, RolInvolucrado = Roles.Docente, IdCarrera = 6 }, // Materias Básicas
                new Involucrados { Id = 6, Nombre = "Ana", Apellido = "Lopez", Email = "ana.lopez@utn.edu.ar", Telefono = "3564554433", Legajo = 67892, RolInvolucrado = Roles.Docente, IdCarrera = 1 }, // Ing. Química
                
                // Secretarios
                new Involucrados { Id = 7, Nombre = "Roberto", Apellido = "Sanchez", Email = "roberto.sanchez@utn.edu.ar", Telefono = "3564443322", Legajo = 89001, RolInvolucrado = Roles.Secretario, IdCarrera = 7 }, // SEU
                new Involucrados { Id = 8, Nombre = "Laura", Apellido = "Diaz", Email = "laura.diaz@utn.edu.ar", Telefono = "3564332211", Legajo = 89002, RolInvolucrado = Roles.Secretario, IdCarrera = 7 }, // SEU
                
                // Externos (sin carrera - null)
                new Involucrados { Id = 9, Nombre = "Pedro", Apellido = "Morales", Email = "pedro.morales@techsolutions.com", Telefono = "3564221100", Legajo = null, RolInvolucrado = Roles.Externo, IdCarrera = null },
                new Involucrados { Id = 10, Nombre = "Gabriela", Apellido = "Torres", Email = "gabriela.torres@innovacion.com", Telefono = "3564110099", Legajo = null, RolInvolucrado = Roles.Externo, IdCarrera = null },
                new Involucrados { Id = 11, Nombre = "Miguel", Apellido = "Ruiz", Email = "miguel.ruiz@electronica.com", Telefono = "3564009988", Legajo = null, RolInvolucrado = Roles.Externo, IdCarrera = null }
            );

            // Seed Archivos Adjuntos - Para Convenios Específicos y Marcos
            modelBuilder.Entity<ArchivosAdjuntos>().HasData(
                // Archivos de Convenios Específicos
                new ArchivosAdjuntos { Id = 1, NombreArchivo = "plan_trabajo.pdf", RutaArchivo = "/uploads/especificos/plan_trabajo.pdf", ContentType = "application/pdf", ConvenioEspecificoId = 1, ConvenioMarcoId = null },
                new ArchivosAdjuntos { Id = 2, NombreArchivo = "acuerdo_confidencialidad.pdf", RutaArchivo = "/uploads/especificos/acuerdo_confidencialidad.pdf", ContentType = "application/pdf", ConvenioEspecificoId = 1, ConvenioMarcoId = null },
                new ArchivosAdjuntos { Id = 3, NombreArchivo = "proyecto_investigacion_ia.docx", RutaArchivo = "/uploads/especificos/proyecto_ia.docx", ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document", ConvenioEspecificoId = 2, ConvenioMarcoId = null },
                new ArchivosAdjuntos { Id = 4, NombreArchivo = "especificaciones_tecnicas.pdf", RutaArchivo = "/uploads/especificos/especificaciones.pdf", ContentType = "application/pdf", ConvenioEspecificoId = 3, ConvenioMarcoId = null },
                new ArchivosAdjuntos { Id = 5, NombreArchivo = "programa_capacitacion.pdf", RutaArchivo = "/uploads/especificos/programa_capacitacion.pdf", ContentType = "application/pdf", ConvenioEspecificoId = 4, ConvenioMarcoId = null },
                new ArchivosAdjuntos { Id = 6, NombreArchivo = "cronograma_actividades.xlsx", RutaArchivo = "/uploads/especificos/cronograma.xlsx", ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ConvenioEspecificoId = 5, ConvenioMarcoId = null },
                new ArchivosAdjuntos { Id = 7, NombreArchivo = "informe_final_pfi.pdf", RutaArchivo = "/uploads/especificos/informe_final.pdf", ContentType = "application/pdf", ConvenioEspecificoId = 6, ConvenioMarcoId = null },
                
                // Archivos de Convenios Marcos
                new ArchivosAdjuntos { Id = 8, NombreArchivo = "convenio_marco_tech.pdf", RutaArchivo = "/uploads/marcos/cm_tech.pdf", ContentType = "application/pdf", ConvenioEspecificoId = null, ConvenioMarcoId = 1 },
                new ArchivosAdjuntos { Id = 9, NombreArchivo = "resolucion_rectoral_001.pdf", RutaArchivo = "/uploads/marcos/res_001.pdf", ContentType = "application/pdf", ConvenioEspecificoId = null, ConvenioMarcoId = 1 },
                new ArchivosAdjuntos { Id = 10, NombreArchivo = "convenio_marco_innovacion.pdf", RutaArchivo = "/uploads/marcos/cm_innovacion.pdf", ContentType = "application/pdf", ConvenioEspecificoId = null, ConvenioMarcoId = 2 },
                new ArchivosAdjuntos { Id = 11, NombreArchivo = "convenio_marco_electronica.pdf", RutaArchivo = "/uploads/marcos/cm_electronica.pdf", ContentType = "application/pdf", ConvenioEspecificoId = null, ConvenioMarcoId = 3 },
                new ArchivosAdjuntos { Id = 12, NombreArchivo = "borrador_convenio_quimica.docx", RutaArchivo = "/uploads/marcos/borrador_quimica.docx", ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document", ConvenioEspecificoId = null, ConvenioMarcoId = 4 }
            );

            // Configurar y Sembrar Many-to-Many: Carreras <-> ConvenioEspecifico
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
                       .OnDelete(DeleteBehavior.Cascade), // opcional
                   j =>
                   {
                       j.HasData(
                           // CE-001: Pasantías 2024 - Sistemas y Programación
                           new { CarreraId = 2, ConvenioEspecificoId = 1 }, // Ing. Sistemas
                           new { CarreraId = 5, ConvenioEspecificoId = 1 }, // Tec. Programación
                           
                           // CE-002: Investigación Conjunta AI - Solo Sistemas
                           new { CarreraId = 2, ConvenioEspecificoId = 2 }, // Ing. Sistemas
                           
                           // CE-003: Desarrollo de Prototipos Electrónicos - Electrónica y Electromecánica
                           new { CarreraId = 3, ConvenioEspecificoId = 3 }, // Ing. Electrónica
                           new { CarreraId = 4, ConvenioEspecificoId = 3 }, // Ing. Electromecánica
                           
                           // CE-004: Capacitación SEU - Todas las carreras + SEU
                           new { CarreraId = 1, ConvenioEspecificoId = 4 }, // Ing. Química
                           new { CarreraId = 2, ConvenioEspecificoId = 4 }, // Ing. Sistemas
                           new { CarreraId = 3, ConvenioEspecificoId = 4 }, // Ing. Electrónica
                           new { CarreraId = 4, ConvenioEspecificoId = 4 }, // Ing. Electromecánica
                           new { CarreraId = 7, ConvenioEspecificoId = 4 }, // SEU
                           
                           // CE-005: Prácticas Profesionales Química - Química y Materias Básicas
                           new { CarreraId = 1, ConvenioEspecificoId = 5 }, // Ing. Química
                           new { CarreraId = 6, ConvenioEspecificoId = 5 }, // Materias Básicas
                           
                           // CE-006: Proyecto Final Integrador 2023 - Electrónica
                           new { CarreraId = 3, ConvenioEspecificoId = 6 }, // Ing. Electrónica
                           
                           // ACTA-001: Acta Acuerdo Vinculación Tecnológica - Sistemas y Vinculación
                           new { CarreraId = 2, ConvenioEspecificoId = 7 }, // Ing. Sistemas
                           new { CarreraId = 8, ConvenioEspecificoId = 7 }, // Vinculación Tecnológica
                           
                           // ACTA-002: Acta Donación Equipamiento - Todas las ingenierías
                           new { CarreraId = 1, ConvenioEspecificoId = 8 }, // Ing. Química
                           new { CarreraId = 2, ConvenioEspecificoId = 8 }, // Ing. Sistemas
                           new { CarreraId = 3, ConvenioEspecificoId = 8 }, // Ing. Electrónica
                           new { CarreraId = 4, ConvenioEspecificoId = 8 }, // Ing. Electromecánica
                           
                           // CE-007: Futuro Convenio Materias Básicas - Materias Básicas
                           new { CarreraId = 6, ConvenioEspecificoId = 9 }  // Materias Básicas
                       );
                   }
               );

            // Configurar y Sembrar Many-to-Many: Involucrados <-> ConvenioEspecifico
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
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasData(
                            // CE-001: Pasantías 2024 - Alumnos y Docente tutor
                            new { InvolucradosId = 1, ConvenioEspecificoId = 1 }, // Juan Perez (Alumno)
                            new { InvolucradosId = 2, ConvenioEspecificoId = 1 }, // Sofia Martinez (Alumno)
                            new { InvolucradosId = 4, ConvenioEspecificoId = 1 }, // Maria Gomez (Docente)
                            new { InvolucradosId = 9, ConvenioEspecificoId = 1 }, // Pedro Morales (Externo - Tech Solutions)
                            
                            // CE-002: Investigación Conjunta AI - Docentes y Externo
                            new { InvolucradosId = 4, ConvenioEspecificoId = 2 }, // Maria Gomez (Docente)
                            new { InvolucradosId = 5, ConvenioEspecificoId = 2 }, // Carlos Fernandez (Docente)
                            new { InvolucradosId = 10, ConvenioEspecificoId = 2 }, // Gabriela Torres (Externo - Innovación)
                            
                            // CE-003: Desarrollo de Prototipos Electrónicos - Alumno, Docente y Externo
                            new { InvolucradosId = 3, ConvenioEspecificoId = 3 }, // Lucas Rodriguez (Alumno)
                            new { InvolucradosId = 6, ConvenioEspecificoId = 3 }, // Ana Lopez (Docente)
                            new { InvolucradosId = 11, ConvenioEspecificoId = 3 }, // Miguel Ruiz (Externo - Electrónica)
                            
                            // CE-004: Capacitación SEU - Secretarios y Docentes
                            new { InvolucradosId = 7, ConvenioEspecificoId = 4 }, // Roberto Sanchez (Secretario)
                            new { InvolucradosId = 8, ConvenioEspecificoId = 4 }, // Laura Diaz (Secretario)
                            new { InvolucradosId = 5, ConvenioEspecificoId = 4 }, // Carlos Fernandez (Docente)
                            
                            // CE-005: Prácticas Profesionales Química - Alumno y Docente
                            new { InvolucradosId = 1, ConvenioEspecificoId = 5 }, // Juan Perez (Alumno)
                            new { InvolucradosId = 6, ConvenioEspecificoId = 5 }, // Ana Lopez (Docente)
                            
                            // CE-006: Proyecto Final Integrador 2023 (Finalizado) - Alumno y Docente
                            new { InvolucradosId = 3, ConvenioEspecificoId = 6 }, // Lucas Rodriguez (Alumno)
                            new { InvolucradosId = 6, ConvenioEspecificoId = 6 }, // Ana Lopez (Docente)
                            
                            // ACTA-001: Acta Acuerdo Vinculación Tecnológica - Secretario, Docente y Externo
                            new { InvolucradosId = 7, ConvenioEspecificoId = 7 }, // Roberto Sanchez (Secretario)
                            new { InvolucradosId = 5, ConvenioEspecificoId = 7 }, // Carlos Fernandez (Docente)
                            new { InvolucradosId = 9, ConvenioEspecificoId = 7 }, // Pedro Morales (Externo)
                            
                            // ACTA-002: Acta Donación Equipamiento - Secretarios
                            new { InvolucradosId = 7, ConvenioEspecificoId = 8 }, // Roberto Sanchez (Secretario)
                            new { InvolucradosId = 8, ConvenioEspecificoId = 8 }, // Laura Diaz (Secretario)
                            
                            // CE-007: Futuro Convenio (Borrador) - Solo Secretario
                            new { InvolucradosId = 8, ConvenioEspecificoId = 9 }  // Laura Diaz (Secretario)
                        );
                    }
                );

            // Configurar relación Involucrados -> Carrera con nombre de FK explícito
            modelBuilder.Entity<Involucrados>()
                .HasOne(i => i.Carrera)
                .WithMany()
                .HasForeignKey(i => i.IdCarrera)
                .IsRequired(false);

            modelBuilder.Entity<Empresa>()
                .HasOne(e => e.ConvenioMarco)
                .WithOne(cm => cm.Empresa)
                .HasForeignKey<ConvenioMarco>(cm => cm.EmpresaId)
                .IsRequired(false);                  

        }
    }
}
