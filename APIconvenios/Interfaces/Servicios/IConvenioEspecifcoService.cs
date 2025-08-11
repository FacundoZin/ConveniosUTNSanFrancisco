using APIconvenios.Common;
using APIconvenios.DTOs.ConvenioEspecifico;
using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.DTOs.Empresa;
using APIconvenios.DTOs.Involucrados;
using APIconvenios.Filters;

namespace APIconvenios.Interfaces.Servicios
{
    public interface IConvenioEspecifcoService
    {
        Task<Result<List<ConvenioEspecificoDto>>> ListarConveniosEspecificos(ConvenioQueryObject queryObject);
        Task<Result<object?>> CreateConvenioEspecifico(InsertConvenioEspecificoDto DtoConvenio,
            List<InsertInvolucradosDto> DtoInvolucrados);
        Task<Result<object?>> DeleteConvenioEspecifico(int id);
        Task<Result<InfoConvenioEspeficoDto>> ObtenerConvenioEspecificoCompleto(int id);
        Task<Result<object?>> EditarConvenioEspecifico(UpdateConvenioEspecificoDto Dto);
    }
}
