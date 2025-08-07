using APIconvenios.Common;
using APIconvenios.DTOs.ConvenioEspecifico;
using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.Filters;

namespace APIconvenios.Interfaces.Servicios
{
    public interface IConvenioEspecifcoService
    {
        Task<Result<List<ConvenioEspecificoDto>>> ListarConveniosEspecificos(ConvenioQueryObject queryObject);
        Task<Result<object?>> CreateConvenioEspecifico(CreateConvenioEspecificoDto Dto);
        Task<Result<object?>> DeleteConvenioEspecifico(int id);
        Task<Result<InfoConvenioMarcoDto>> ObtenerConvenioEspecificoCompleto(int id);
        Task<Result<object?>> EditarConvenioEspecifico(UpdateConvenioEspecificoDto Dto);
    }
}
