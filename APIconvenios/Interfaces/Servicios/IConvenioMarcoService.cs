using APIconvenios.Common;
using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.Filters;

namespace APIconvenios.Interfaces.Servicios
{
    public interface IConvenioMarcoService
    {
        Task<Result<List<ListaConveniosMarcosDto>>> ListarConveniosMarcos(ConvenioQueryObject queryObject);
        Task<InfoConvenioMarcoDto?> ObtenerConvenioMarcoCompleto(int id);
        Task BorrarConvenioMarco(int id);
        Task ActualizarConvenioMarco(UpdateConvenioMarcoDto convenioActualizado);
    }
}
