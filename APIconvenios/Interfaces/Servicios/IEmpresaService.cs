using APIconvenios.Common;
using APIconvenios.DTOs.Empresa;

namespace APIconvenios.Interfaces.Servicios
{
    public interface IEmpresaService
    {
        Task<PaginatedResult<List<ComboBoxEmpresasDto>>> ListarEmpresasPaginado(int pagina, int cantidad);
        Task<Result<int>> CrearEmpresa(InsertEmpresaDto dto);
        Task<Result<bool>> EditarEmpresa(int idEmpresa, EditEmpresaDto dto);
        Task<Result<List<ComboBoxEmpresasDto>>> ListarTodasLasEmpresas();
    }
}
