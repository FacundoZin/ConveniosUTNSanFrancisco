using APIconvenios.Common;

namespace APIconvenios.Interfaces.Servicios
{
    public interface IConvenioFilterService
    {
        Task<Result<object>> ListarConvenios(ConvenioQueryObject queryObject);
    }
}
