using APIconvenios.DTOs.ConvenioEspecifico;
using APIconvenios.DTOs.Involucrados;
using APIconvenios.Models;
using System.Runtime.CompilerServices;

namespace APIconvenios.Helpers.Mappers
{
    public static class ConvenioEspecificoMapper
    {
        public static ConvenioEspecifico UploadData (this InsertConvenioEspecificoDto dto)
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

        public static ConvenioEspecifico UpdateConvenio (this ConvenioEspecifico ConvenioOriginal, UpdateConvenioEspecificoDto dto)
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

        public static List<ConvenioEspecificoDto> ToDto (this List<ConvenioEspecifico> convenios)
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
    }
}
