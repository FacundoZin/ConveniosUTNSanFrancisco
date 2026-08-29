using APIconvenios.Models;
using APIconvenios.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            Console.WriteLine("Eliminando datos existentes para re-sembrar...");
            context.ArchivosAdjuntos.ExecuteDelete();
            context.ConveniosEspecificos.ExecuteDelete();
            context.ConveniosMarcos.ExecuteDelete();
            context.Involucrados.ExecuteDelete();
            context.Empresas.ExecuteDelete();
            Console.WriteLine("Datos eliminados correctamente.");

            Console.WriteLine("Iniciando Seeding de datos...");

            // 1. Crear Empresas
            var nombresEmpresas = new[] { "Tech", "Soft", "Mecánica", "Alimentos", "Construcciones", "Global", "Data", "Cloud", "Agro", "Innova" };
            var sufijos = new[] { "Solutions", "Inc.", "S.A.", "S.R.L.", "Group", "Services" };
            var empresas = new List<Empresa>();
            var random = new Random();

            for (int i = 0; i < 20; i++)
            {
                var nombre = $"{nombresEmpresas[random.Next(nombresEmpresas.Length)]} {sufijos[random.Next(sufijos.Length)]} {i + 1}";
                empresas.Add(new Empresa
                {
                    Nombre = nombre,
                    RazonSocial = $"{nombre} Corp.",
                    Cuit = $"30-{random.Next(10000000, 99999999)}-{random.Next(0, 9)}",
                    Direccion = $"Calle {random.Next(1, 999)} Nro {random.Next(10, 5000)}",
                    Telefono = $"3564-{random.Next(100000, 999999)}",
                    Email = $"contacto@empresa{i + 1}.com"
                });
            }

            context.Empresas.AddRange(empresas);
            context.SaveChanges();

            // 2. Crear Convenios Marcos
            var conveniosMarcos = new List<ConvenioMarco>();
            for (int i = 0; i < empresas.Count; i++)
            {
                var cm = new ConvenioMarco
                {
                    numeroconvenio = $"CM-2024-{i + 1:D3}",
                    Titulo = $"Convenio Marco de Cooperación con {empresas[i].Nombre}",
                    FechaFirmaConvenio = DateOnly.FromDateTime(DateTime.Now.AddDays(-random.Next(100, 1000))),
                    FechaFin = DateOnly.FromDateTime(DateTime.Now.AddDays(random.Next(100, 1000))),
                    Estado = random.Next(10) > 7 ? EstadoConvenio.Borrador : (random.Next(10) > 3 ? EstadoConvenio.Vigente : EstadoConvenio.Finalizado),
                    NumeroResolucion = $"RES-CD-{i + 1:D3}/2024",
                    Refrendado = random.Next(10) > 2,
                    EmpresaId = empresas[i].Id
                };
                conveniosMarcos.Add(cm);
            }
            context.ConveniosMarcos.AddRange(conveniosMarcos);
            context.SaveChanges();

            // 3. Obtener Carreras (ya sembradas en OnModelCreating)
            var carreras = context.Carreras.ToList();

            // 4. Crear Involucrados
            var nombres = new[] { "Juan", "Maria", "Carlos", "Ana", "Luis", "Sofia", "Pedro", "Elena", "Ramiro", "Lucia" };
            var apellidos = new[] { "Perez", "Gomez", "Robledo", "Lopez", "Martinez", "Rodriguez", "Fernandez", "Vazquez", "Diaz", "Muller" };
            var involucrados = new List<Involucrados>();
            for (int i = 0; i < 15; i++)
            {
                involucrados.Add(new Involucrados
                {
                    Nombre = nombres[random.Next(nombres.Length)],
                    Apellido = apellidos[random.Next(apellidos.Length)],
                    Email = $"user{i}@utn.edu.ar",
                    Telefono = $"+54911{100000 + i:D6}",
                    Legajo = random.Next(10000, 99999),
                    RolInvolucrado = (Roles)random.Next(0, 5),
                    IdCarrera = carreras[random.Next(carreras.Count)].Id
                });
            }
            // Ensure Nombre+Apellido+Telefono uniqueness (hash collision guard)
            var uniqueKeys = new HashSet<string>();
            foreach (var inv in involucrados)
            {
                var key = $"{inv.Nombre.ToLower().Trim()}|{inv.Apellido.ToLower().Trim()}|{inv.Telefono.ToLower().Trim()}";
                if (!uniqueKeys.Add(key))
                {
                    inv.Telefono = $"+54911{200000 + uniqueKeys.Count:D6}";
                    uniqueKeys.Add($"{inv.Nombre.ToLower().Trim()}|{inv.Apellido.ToLower().Trim()}|{inv.Telefono.ToLower().Trim()}");
                }
            }
            context.Involucrados.AddRange(involucrados);
            context.SaveChanges();

            // 5. Crear Convenios Específicos
            var conveniosEspecificos = new List<ConvenioEspecifico>();
            string[] tipos = { "Pasantías", "Asistencia Técnica", "Capacitación", "Investigación", "Práctica Profesional" };

            int ceCount = 1;
            foreach (var cm in conveniosMarcos)
            {
                int numSpecifics = random.Next(2, 5); // Entre 2 y 4 específicos por cada marco
                for (int j = 0; j < numSpecifics; j++)
                {
                    var ce = new ConvenioEspecifico
                    {
                        numeroconvenio = $"CE-2024-{ceCount:D3}",
                        TituloConvenio = $"{tipos[random.Next(tipos.Length)]} - {cm.Empresa.Nombre} - {j + 1}",
                        FechaFirmaConvenio = cm.FechaFirmaConvenio?.AddMonths(random.Next(1, 3)),
                        FechaInicioActividades = cm.FechaFirmaConvenio?.AddMonths(random.Next(3, 4)),
                        FechaFinConvenio = cm.FechaFin?.AddMonths(-random.Next(1, 6)),
                        Estado = random.Next(10) > 3 ? EstadoConvenio.Vigente : EstadoConvenio.Finalizado,
                        EsActa = random.Next(10) > 7,
                        NumeroResolucion = $"RES-CD-CE-{ceCount:D3}/2024",
                        Refrendado = random.Next(10) > 1,
                        ConvenioMarcoId = cm.Id,
                        EmpresaId = cm.EmpresaId
                    };

                    // Asignar Carreras aleatorias
                    var carrerasSeleccionadas = carreras.OrderBy(x => random.Next()).Take(random.Next(1, 3)).ToList();
                    foreach (var car in carrerasSeleccionadas) ce.CarrerasInvolucradas.Add(car);

                    // Asignar Involucrados aleatorios
                    var invSeleccionados = involucrados.OrderBy(x => random.Next()).Take(random.Next(1, 3)).ToList();
                    foreach (var inv in invSeleccionados) ce.Involucrados.Add(inv);

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
                    NombreArchivo = $"PDF_CM_{cm.numeroconvenio}.pdf",
                    RutaArchivo = $"/uploads/marcos/{cm.numeroconvenio}.pdf",
                    ContentType = "application/pdf",
                    ConvenioMarcoId = cm.Id
                });
            }

            foreach (var ce in conveniosEspecificos.OrderBy(x => random.Next()).Take(20))
            {
                archivos.Add(new ArchivosAdjuntos
                {
                    NombreArchivo = $"Anexo_{ce.numeroconvenio}.pdf",
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
            Console.WriteLine($"- {conveniosEspecificos.Count} Convenios Específicos (Total: {ceCount - 1})");
            Console.WriteLine($"- {involucrados.Count} Involucrados");
            Console.WriteLine($"- {archivos.Count} Archivos Adjuntos");
        }
    }
}
