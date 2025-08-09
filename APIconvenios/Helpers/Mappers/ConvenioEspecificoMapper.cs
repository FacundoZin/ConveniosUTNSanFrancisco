using APIconvenios.DTOs.ConvenioEspecifico;
using APIconvenios.DTOs.Involucrados;
using APIconvenios.Models;
using System.Runtime.CompilerServices;

namespace APIconvenios.Helpers.Mappers
{
    public static class ConvenioEspecificoMapper
    {
        public static ConvenioEspecifico ToConvenioEspecifico (this InsertConvenioEspecificoDto dto, 
            List<InsertInvolucradosDto>  involucradosDtos)
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

                Involucrados = involucradosDtos.Select(i =>  new Involucrados
                {
                    Id = i.id == 
                    Nombre = i.Nombre,
                    Apellido = i.Apellido,
                    Email = i.Email,
                    Telefono = i.Telefono,
                    Legajo = i.Legajo,
                    RolInvolucrado = i.RolInvolucrado
                }).ToList() 
            };
        }
    }
}
