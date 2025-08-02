using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.Models;

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
    }
}
