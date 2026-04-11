using APIconvenios.Models;
using APIconvenios.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            // Solo seedear si no hay datos significativos (excepto Carreras que ya tienen seed fijo)
            if (context.Empresas.Any() || context.ConveniosMarcos.Any() || context.ConveniosEspecificos.Any())
            {
                Console.WriteLine("El base de datos ya contiene datos. Saltando seeder...");
                return;
            }

            Console.WriteLine("Iniciando Seeding de datos...");

            // 1. Crear Empresas
            var empresas = new List<Empresa>
            {
                new Empresa { Nombre = "Tech Solutions S.A.", RazonSocial = "Tech Solutions Sociedad Anónima", Cuit = "30-12345678-9", Direccion = "Av. Siempre Viva 123", Telefono = "3564-112233", Email = "info@techsolutions.com" },
                new Empresa { Nombre = "SoftDev Inc.", RazonSocial = "Software Development Incorporated", Cuit = "30-87654321-5", Direccion = "Calle Falsa 456", Telefono = "3564-445566", Email = "contact@softdev.io" },
                new Empresa { Nombre = "Mecánica San Francisco", RazonSocial = "Mecánica SF S.R.L.", Cuit = "20-11223344-2", Direccion = "Bv. 25 de Mayo 1000", Telefono = "3564-778899", Email = "mecanica@sf.com.ar" },
                new Empresa { Nombre = "Alimentos Globales", RazonSocial = "Global Foods S.A.", Cuit = "33-55667788-0", Direccion = "Parque Industrial Lote 5", Telefono = "3564-156001", Email = "rrhh@globalfoods.com" },
                new Empresa { Nombre = "Construcciones Modernas", RazonSocial = "Modern Build S.A.", Cuit = "30-99887766-1", Direccion = "Av. de la Universidad 500", Telefono = "3564-159988", Email = "obras@modernbuild.com" }
            };

            context.Empresas.AddRange(empresas);
            context.SaveChanges();

            // 2. Crear Convenios Marcos
            var conveniosMarcos = new List<ConvenioMarco>();
            for (int i = 0; i < empresas.Count; i++)
            {
                var cm = new ConvenioMarco
                {
                    numeroconvenio = $"CM-2024-00{i + 1}",
                    Titulo = $"Convenio Marco de Cooperación con {empresas[i].Nombre}",
                    FechaFirmaConvenio = DateOnly.FromDateTime(DateTime.Now.AddMonths(-12 + i)),
                    FechaFin = DateOnly.FromDateTime(DateTime.Now.AddMonths(24 + i)),
                    Estado = i % 3 == 0 ? EstadoConvenio.Borrador : EstadoConvenio.Vigente,
                    NumeroResolucion = $"RES-CD-00{i + 1}/2024",
                    Refrendado = true,
                    EmpresaId = empresas[i].Id
                };
                conveniosMarcos.Add(cm);
            }
            context.ConveniosMarcos.AddRange(conveniosMarcos);
            context.SaveChanges();

            // 3. Obtener Carreras (ya sembradas en OnModelCreating)
            var carreras = context.Carreras.ToList();

            // 4. Crear Involucrados
            var involucrados = new List<Involucrados>
            {
                new Involucrados { Nombre = "Juan", Apellido = "Perez", Email = "juan.perez@utn.edu.ar", Telefono = "3564-101010", Legajo = 15423, RolInvolucrado = Roles.Docente, IdCarrera = 2 },
                new Involucrados { Nombre = "Maria", Apellido = "Gomez", Email = "m.gomez@gmail.com", Telefono = "3564-202020", Legajo = 55123, RolInvolucrado = Roles.Alumno, IdCarrera = 2 },
                new Involucrados { Nombre = "Carlos", Apellido = "Robledo", Email = "carlos.robledo@utn.edu.ar", Telefono = "3564-303030", Legajo = 10052, RolInvolucrado = Roles.Docente, IdCarrera = 1 },
                new Involucrados { Nombre = "Ana", Apellido = "López", Email = "ana.lopez@externo.com", Telefono = "3564-404040", RolInvolucrado = Roles.Externo },
                new Involucrados { Nombre = "Luis", Apellido = "Martinez", Email = "luis.martinez@utn.edu.ar", Telefono = "3564-505050", Legajo = 44556, RolInvolucrado = Roles.Secretario, IdCarrera = 7 },
                new Involucrados { Nombre = "Sofia", Apellido = "Rodriguez", Email = "sofia.rod@alumno.utn.edu.ar", Telefono = "3564-606060", Legajo = 56789, RolInvolucrado = Roles.Alumno, IdCarrera = 5 }
            };
            context.Involucrados.AddRange(involucrados);
            context.SaveChanges();

            // 5. Crear Convenios Específicos
            var conveniosEspecificos = new List<ConvenioEspecifico>();
            string[] tipos = { "Pasantías", "Asistencia Técnica", "Capacitación", "Investigación" };

            int ceCount = 1;
            foreach (var cm in conveniosMarcos)
            {
                for (int j = 0; j < 2; j++) // 2 específicos por cada marco
                {
                    var ce = new ConvenioEspecifico
                    {
                        numeroconvenio = $"CE-2024-{ceCount:D3}",
                        TituloConvenio = $"{tipos[ceCount % tipos.Length]} - {cm.Empresa.Nombre}",
                        FechaFirmaConvenio = cm.FechaFirmaConvenio?.AddMonths(1),
                        FechaInicioActividades = cm.FechaFirmaConvenio?.AddMonths(2),
                        FechaFinConvenio = cm.FechaFin?.AddMonths(-1),
                        Estado = ceCount % 5 == 0 ? EstadoConvenio.Finalizado : EstadoConvenio.Vigente,
                        EsActa = ceCount % 3 == 0,
                        NumeroResolucion = $"RES-CD-CE-{ceCount:D3}/2024",
                        Refrendado = true,
                        ConvenioMarcoId = cm.Id,
                        EmpresaId = cm.EmpresaId
                    };

                    // Asignar Carreras aleatorias
                    var random = new Random();
                    var carrerasSeleccionadas = carreras.OrderBy(x => random.Next()).Take(2).ToList();
                    foreach (var car in carrerasSeleccionadas)
                    {
                        ce.CarrerasInvolucradas.Add(car);
                    }

                    // Asignar Involucrados aleatorios
                    var invSeleccionados = involucrados.OrderBy(x => random.Next()).Take(2).ToList();
                    foreach (var inv in invSeleccionados)
                    {
                        ce.Involucrados.Add(inv);
                    }

                    conveniosEspecificos.Add(ce);
                    ceCount++;
                }
            }

            context.ConveniosEspecificos.AddRange(conveniosEspecificos);
            context.SaveChanges();

            // 6. Archivos Adjuntos Simulados
            var archivos = new List<ArchivosAdjuntos>();
            foreach (var cm in conveniosMarcos)
            {
                archivos.Add(new ArchivosAdjuntos
                {
                    NombreArchivo = $"PDF_Convenio_Marco_{cm.numeroconvenio}.pdf",
                    RutaArchivo = $"/uploads/marcos/{cm.numeroconvenio}.pdf",
                    ContentType = "application/pdf",
                    ConvenioMarcoId = cm.Id
                });
            }

            foreach (var ce in conveniosEspecificos.Take(10))
            {
                archivos.Add(new ArchivosAdjuntos
                {
                    NombreArchivo = $"Anexo_Tecnico_{ce.numeroconvenio}.pdf",
                    RutaArchivo = $"/uploads/especificos/anexo_{ce.numeroconvenio}.pdf",
                    ContentType = "application/pdf",
                    ConvenioEspecificoId = ce.Id
                });
            }

            context.ArchivosAdjuntos.AddRange(archivos);
            context.SaveChanges();

            Console.WriteLine($"Seeding completado con éxito:");
            Console.WriteLine($"- {empresas.Count} Empresas");
            Console.WriteLine($"- {conveniosMarcos.Count} Convenios Marcos");
            Console.WriteLine($"- {conveniosEspecificos.Count} Convenios Específicos");
            Console.WriteLine($"- {involucrados.Count} Involucrados");
            Console.WriteLine($"- {archivos.Count} Archivos Adjuntos");
        }
    }
}
