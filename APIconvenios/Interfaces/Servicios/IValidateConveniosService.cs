using APIconvenios.Common;
using APIconvenios.DTOs.ConvenioEspecifico;
using APIconvenios.DTOs.ConvenioMarco;

namespace APIconvenios.Interfaces.Servicios
{
    public interface IValidateConveniosService
    {
        Task<Result<object?>> ValidateCargaConvenioMarco(CargarConvenioMarcoRequestDto _Dto);
        Task<Result<object?>> ValidateCargaConvenioEspecifico(CargarConvenioEspecificoRequestDto _Dto);
        Task<Result<object?>> ValidateUpdateConvenioMarco(UpdateConvenioMarcoRequetsDto _Dto);
        Task<Result<object?>> ValidateUpdateConvenioEspecifico(UpdateConvenioEspecificoRequestDto _Dto);
    }
}
