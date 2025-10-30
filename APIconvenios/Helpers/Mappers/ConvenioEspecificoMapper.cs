using APIconvenios.DTOs.Archivo;
using APIconvenios.DTOs.ConvenioEspecifico;
using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.DTOs.Empresa;
using APIconvenios.DTOs.Involucrados;
using APIconvenios.Models;
using System.Runtime.CompilerServices;

namespace APIconvenios.Helpers.Mappers
{
    public static class ConvenioEspecificoMapper
    {
        public static ConvenioEspecifico UploadData(this InsertConvenioEspecificoDto dto)
        {
            return new ConvenioEspecifico
            {
                TituloConvenio = dto.Titulo,
                numeroconvenio = dto.numeroconvenio,
                FechaFirmaConvenio = dto.FechaFirmaConvenio,
                FechaInicioActividades = dto.FechaInicioActividades,
                FechaFinConvenio = dto.FechaFinConvenio,
                ComentarioOpcional = dto.ComentarioOpcional,
                Estado = dto.Estado,
                EsActa = dto.EsActa,
                Refrendado = dto.Refrendado,
                NumeroResolucion = dto.NumeroResolucion,
            };
        }

        public static ConvenioEspecifico UpdateConvenio(this ConvenioEspecifico ConvenioOriginal, UpdateConvenioEspecificoDto dto)
        {
            ConvenioOriginal.TituloConvenio = dto.Titulo;
            ConvenioOriginal.numeroconvenio = dto.numeroconvenio;
            ConvenioOriginal.FechaFirmaConvenio = dto.FechaFirmaConvenio;
            ConvenioOriginal.FechaInicioActividades = dto.FechaInicioActividades;
            ConvenioOriginal.FechaFinConvenio = dto.FechaFinConvenio;
            ConvenioOriginal.ComentarioOpcional = dto.ComentarioOpcional;
            ConvenioOriginal.Estado = dto.Estado;
            ConvenioOriginal.EsActa = dto.EsActa;
            ConvenioOriginal.Refrendado = dto.Refrendado;
            ConvenioOriginal.NumeroResolucion = dto.NumeroResolucion;

            return ConvenioOriginal;
        }

        public static List<ConvenioEspecificoDto> ToDto(this List<ConvenioEspecifico> convenios)
        {
            return convenios.Select(c => new ConvenioEspecificoDto
            {
                Id = c.Id,
                numeroconvenio = c.numeroconvenio,
                Titulo = c.TituloConvenio,
                NombreEmpresa = c.empresa?.Nombre,
                FechaFirmaConvenio = c.FechaFirmaConvenio,
                FechaInicioActividades = c.FechaInicioActividades,
                FechaFin = c.FechaFinConvenio,
                Estado = c.Estado,
                EsActa = c.EsActa,
                Refrendado = c.Refrendado
            }).ToList();
        }

        public static InfoConvenioEspeficoDto ToFullInfo(this ConvenioEspecifico convenio)
        {
            return new InfoConvenioEspeficoDto
            {
                Id = convenio.Id,
                numeroconvenio = convenio.numeroconvenio,
                Titulo = convenio.TituloConvenio,
                FechaFirmaConvenio = convenio.FechaFirmaConvenio,
                FechaInicioActividades = convenio.FechaInicioActividades,
                FechaFinConvenio = convenio.FechaFinConvenio,
                ComentarioOpcional = convenio.ComentarioOpcional,
                Estado = convenio.Estado,
                EsActa = convenio.EsActa,
                NumeroResolucion = convenio.NumeroResolucion,
                Refrendado = convenio.Refrendado,

                CarrerasInvolucradas = convenio.CarrerasInvolucradas?.Select(c => new Carreras
                {
                    Id = c.Id,
                    Nombre = c.Nombre
                }).ToList(),

                ConvenioMarcoId = convenio.ConvenioMarcoId,
                convenioMarco = convenio.ConvenioMarco != null ? new ConvenioMarcoDto
                {
                    Id = convenio.Id,
                    Titulo = convenio.TituloConvenio,
                    numeroconvenio = convenio.numeroconvenio,
                    FechaFirmaConvenio = convenio.FechaFinConvenio,
                    FechaFin = convenio.FechaFinConvenio,
                    Estado = convenio.Estado,
                    Refrendado = convenio.Refrendado
                } : null,

                empresa = convenio.empresa != null ? new EmpresaDto
                {
                    Id = convenio.empresa!.Id,
                    Nombre_Empresa = convenio.empresa.Nombre,
                    RazonSocial = convenio.empresa.RazonSocial,
                    Cuit = convenio.empresa.Cuit,
                    Direccion_Empresa = convenio.empresa.Direccion,
                    Telefono_Empresa = convenio.empresa.Telefono,
                    Email_Empresa = convenio.empresa.Email
                } : null,

                Involucrados = convenio.Involucrados?.Select(i => new InvolucradosDto
                {
                    Id = i.Id,
                    Nombre = i.Nombre,
                    Apellido = i.Apellido,
                    RolInvolucrado = i.RolInvolucrado.ToString(),
                    Email = i.Email,
                    Telefono = i.Telefono,
                    Legajo = i.Legajo
                }).ToList(),

                DocumentosAdjuntos = convenio.ArchivosAdjuntos?.Select(a => new viewArchivoDto
                {
                    IdArchivo = a.Id,
                    NombreArchivo = a.NombreArchivo,
                }).ToList()
            };
        }
    }
}
