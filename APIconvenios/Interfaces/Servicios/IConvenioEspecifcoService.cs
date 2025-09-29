using APIconvenios.Common;
using APIconvenios.DTOs.ConvenioEspecifico;
using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.DTOs.Convenios;
using APIconvenios.DTOs.Empresa;
using APIconvenios.DTOs.Involucrados;

namespace APIconvenios.Interfaces.Servicios
{
    public interface IConvenioEspecifcoService
    {
        Task<Result<ConvenioCreated>> CreateConvenioEspecifico(CargarConvenioEspecificoRequestDto Dto);
        Task<Result<object?>> DeleteConvenioEspecifico(int id);
        Task<Result<InfoConvenioEspeficoDto>> ObtenerConvenioEspecificoCompleto(int id);
        Task<Result<object?>> EditarConvenioEspecifico(UpdateConvenioEspecificoRequestDto Dto);
        Task<Result<object>> ObtenerConveniosEspecificos(ConvenioQueryObject Dto);
    }
}
