using APIconvenios.Common;
using APIconvenios.DTOs.Empresa;
using APIconvenios.DTOs.Involucrado;

namespace APIconvenios.Interfaces.Servicios
{
    public interface IConvenioGetterService
    {
        Task<Result<EmpresaWithConveniosDto>> ListarConveniosPorEmpresa(int empresaId);
        Task<Result<InvolucradosWithConveniosDto>> ListarConveniosPorInvolucrado(int involucradoId);
    }
}
