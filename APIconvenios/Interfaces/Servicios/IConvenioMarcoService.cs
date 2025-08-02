using APIconvenios.Common;
using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.Filters;

namespace APIconvenios.Interfaces.Servicios
{
    public interface IConvenioMarcoService
    {
        Task<Result<List<ListaConveniosMarcosDto>>> ListarConveniosMarcos(ConvenioQueryObject queryObject);
        Task<Result<InfoConvenioMarcoDto?>> ObtenerConvenioMarcoCompleto(int id);
        Task<Result<bool>> BorrarConvenioMarco(int id);
        Task<Result<bool>> ActualizarConvenioMarco(UpdateConvenioMarcoDto convenioActualizado);
    }
}
