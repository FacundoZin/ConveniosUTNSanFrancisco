using APIconvenios.Common;
using APIconvenios.DTOs.ConvenioEspecifico;

namespace APIconvenios.Interfaces.Servicios
{
    public interface IConvenioEspecificoService
    {
        Task<Result<bool>> CreateConvenioEspecifico(CreateConvenioEspecificoDto dto);
        Task<Result<bool>> UpdateConvenioEspecifico(UpdateConvenioEspecificoDto dto);

        Task<Result<bool>> DeleteConvenioEspecifico(int IdConvenio);
    }
}
