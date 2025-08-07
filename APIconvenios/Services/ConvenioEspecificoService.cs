using APIconvenios.Common;
using APIconvenios.DTOs.ConvenioEspecifico;
using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.Filters;
using APIconvenios.Interfaces.Servicios;

namespace APIconvenios.Services
{
    public class ConvenioEspecificoService : IConvenioEspecifcoService
    {
        public Task<Result<object?>> CreateConvenioEspecifico(CreateConvenioEspecificoDto Dto)
        {
            throw new NotImplementedException();
        }

        public Task<Result<object?>> DeleteConvenioEspecifico(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<object?>> EditarConvenioEspecifico(UpdateConvenioEspecificoDto Dto)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<ConvenioEspecificoDto>>> ListarConveniosEspecificos(ConvenioQueryObject queryObject)
        {
            throw new NotImplementedException();
        }

        public Task<Result<InfoConvenioMarcoDto>> ObtenerConvenioEspecificoCompleto(int id)
        {
            throw new NotImplementedException();
        }
    }
}
