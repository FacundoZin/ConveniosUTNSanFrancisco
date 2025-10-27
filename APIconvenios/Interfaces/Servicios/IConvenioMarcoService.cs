using APIconvenios.Common;
using APIconvenios.DTOs.Archivo;
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
        Task<Result<bool>> DesvincularEspecifico(int IdMarco, int IdEspecifico);
        Task<Result<bool>> DesvincularEmpresa(int IdMarco);
        Task<Result<List<viewArchivoDto>>> ObtenerArchivosDeConvenioMarco(int idConvenio);

        Task<Result<int>> GetIdByNumeroConvenio(string Numero);
    }
}
