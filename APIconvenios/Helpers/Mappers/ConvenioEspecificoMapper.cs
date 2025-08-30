using APIconvenios.DTOs.ConvenioEspecifico;
using APIconvenios.DTOs.Involucrados;
using APIconvenios.Models;
using System.Runtime.CompilerServices;

namespace APIconvenios.Helpers.Mappers
{
    public static class ConvenioEspecificoMapper
    {
        public static ConvenioEspecifico ToConvenioEspecifico (this InsertConvenioEspecificoDto dto, 
            List<Involucrados>  involucrados)
        {
            return new ConvenioEspecifico
            {
                Titulo = dto.Titulo,
                numeroconvenio = dto.numeroconvenio,
                FechaFirmaConvenio = dto.FechaFirmaConvenio,
                FechaInicioActividades = dto.FechaInicioActividades,
                FechaFinConvenio = dto.FechaFinConvenio,
                ConvenioMarcoId = dto.ConvenioMarcoId,
                ComentarioOpcional = dto.ComentarioOpcional,
                Involucrados = involucrados
            };
        }

        public static ConvenioEspecifico UpdateConvenio (this ConvenioEspecifico ConvenioOriginal, UpdateConvenioEspecificoDto dto)
        {
            ConvenioOriginal.Titulo = dto.Titulo;
            ConvenioOriginal.numeroconvenio = dto.numeroconvenio;
            ConvenioOriginal.FechaFirmaConvenio = dto.FechaFirmaConvenio;
            ConvenioOriginal.FechaInicioActividades = dto.FechaInicioActividades;
            ConvenioOriginal.FechaFinConvenio = dto.FechaFinConvenio;
            ConvenioOriginal.ComentarioOpcional = dto.ComentarioOpcional;

            return ConvenioOriginal;
        }

        public static List<ConvenioEspecificoDto> ToDto (this List<ConvenioEspecifico> convenios)
        {
            return convenios.Select(c => new ConvenioEspecificoDto
            {
                Id = c.Id,
                numeroconvenio = c.numeroconvenio,
                Titulo = c.Titulo,
                FechaFirmaConvenio = c.FechaFirmaConvenio,
                FechaInicioActividades = c.FechaInicioActividades,
                FechaFin = c.FechaFinConvenio
            }).ToList();
        }
    }
}
