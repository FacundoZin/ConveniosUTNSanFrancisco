using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.Models;
using System.Runtime.CompilerServices;

namespace APIconvenios.Helpers.Mappers
{
    public static class ConvenioMarcoMappers
    {
        public static List<ListaConveniosMarcosDto> ConvertToList(this List<ConvenioMarco> convenios)
        {
            List<ListaConveniosMarcosDto> conveniosDto = new List<ListaConveniosMarcosDto>();

            foreach (var Convenio in convenios)
            {
                conveniosDto.Add(new ListaConveniosMarcosDto
                {
                    Id = Convenio.Id,
                    Titulo = Convenio.Titulo,
                    numeroconvenio = Convenio.numeroconvenio,
                    NombreEmpresa = Convenio.Empresa.Nombre,
                    FechaFirmaConvenio = Convenio.FechaFirmaConvenio,
                    FechaFin = Convenio.FechaFin,
                    ComentarioOpcional = Convenio.ComentarioOpcional
                });
            }

            return conveniosDto;
        }

        public static ConvenioMarco UpdateConvenio(this ConvenioMarco convenio, UpdateConvenioMarcoDto convenioActualizado)
        {
            convenio.numeroconvenio = convenioActualizado.numeroconvenio;
            convenio.Titulo = convenioActualizado.Titulo;
            convenio.FechaFirmaConvenio = convenioActualizado.FechaFirmaConvenio;
            convenio.FechaFin = convenioActualizado.FechaFin;
            convenio.ComentarioOpcional = convenioActualizado.ComentarioOpcional;

            return convenio;
        }

        public static ConvenioMarco ConverToConvenioMarco(this CreateConvenioMarcoDto ConvenioDto)
        {
            return new ConvenioMarco
            {
                numeroconvenio = ConvenioDto.numeroconvenio,
                Titulo = ConvenioDto.Titulo,
                EmpresaId = ConvenioDto.EmpresaId,
                FechaFirmaConvenio = ConvenioDto.FechaFirmaConvenio,
                FechaFin = ConvenioDto.FechaFin,
                ComentarioOpcional = ConvenioDto.ComentarioOpcional,
            };
        }
    }
}
