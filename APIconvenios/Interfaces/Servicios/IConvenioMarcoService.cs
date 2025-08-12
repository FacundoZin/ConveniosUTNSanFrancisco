using APIconvenios.Common;
using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.DTOs.Empresa;
using APIconvenios.Filters;

namespace APIconvenios.Interfaces.Servicios
{
    public interface IConvenioMarcoService
    {
        Task<Result<List<ConvenioMarcoDto>>> ListarConveniosMarcos(ConvenioQueryObject queryObject);
        Task<Result<InfoConvenioMarcoDto?>> ObtenerConvenioMarcoCompleto(int id);
        Task<Result<bool>> BorrarConvenioMarco(int id);
        Task<Result<bool>> ActualizarConvenioMarco(UpdateConvenioMarcoDto convenioActualizado);
        Task<Result<bool>> CargarConvenioMarco(CreateConvenioMarcoDto createConvenioMarcoDto,InsertEmpresaDto EmpresaDto);

    }
}
