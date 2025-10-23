using APIconvenios.Common;
using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.DTOs.Convenios;

namespace APIconvenios.Interfaces.Servicios
{
    public interface IConvenioMarcoService
    {
        Task<Result<InfoConvenioMarcoDto?>> ObtenerConvenioMarcoCompleto(int id);
        Task<Result<bool>> BorrarConvenioMarco(int id);
        Task<Result<bool>> ActualizarConvenioMarco(UpdateConvenioMarcoRequetsDto requetsDto);
        Task<Result<ConvenioCreated>> CargarConvenioMarco(CargarConvenioMarcoRequestDto requestDto);

        Task<Result<int>> GetIdByNumeroConvenio(string Numero);
    }
}
