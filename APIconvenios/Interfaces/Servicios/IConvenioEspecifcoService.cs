using APIconvenios.Common;
using APIconvenios.DTOs.Archivo;
using APIconvenios.DTOs.ConvenioEspecifico;
using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.DTOs.Convenios;
using APIconvenios.DTOs.Empresa;
using APIconvenios.DTOs.Involucrados;
using APIconvenios.Models;

namespace APIconvenios.Interfaces.Servicios
{
    public interface IConvenioEspecifcoService
    {
        Task<Result<List<ComboBoxConvenioEspecificoDto>>> GetAllConveniosEspecificos();
        Task<Result<ConvenioCreated>> CreateConvenioEspecifico(CargarConvenioEspecificoRequestDto Dto);
        Task<Result<object?>> DeleteConvenioEspecifico(int id);
        Task<Result<InfoConvenioEspeficoDto>> ObtenerConvenioEspecificoCompleto(int id);
        Task<Result<object?>> EditarConvenioEspecifico(UpdateConvenioEspecificoRequestDto Dto);
        Task<Result<bool>> DesvincularEmpresa(int IdConvEspecifico);
        Task<Result<bool>> DesvincularMarco(int IdConvEspecifico);
        Task<Result<List<viewArchivoDto>>> ObtenerArchivosDeConvenioEspecifico(int idConvenio);

        Task<Result<int>> GetIdByNumeroConvenio(string NumeroConvenio);
    }
}
