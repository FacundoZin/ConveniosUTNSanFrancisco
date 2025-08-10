using APIconvenios.DTOs.ConvenioEspecifico;
using APIconvenios.Models;
using System.Linq.Expressions;

namespace APIconvenios.Interfaces.Repositorio
{
    public interface IConvenioEspecificoReadRepository
    {
        Task<List<ConvenioEspecificoDto>> ListarConveniosEspecificos(int SaltoPaginas, int CantidadPaginas,
            Expression<Func<ConvenioMarco, bool>> filtro,
            Func<IQueryable<ConvenioMarco>, IOrderedQueryable<ConvenioMarco>>? ordenamiento = null);

        Task<InfoConvenioEspeficoDto> GetConvenioEspecificoCompleto(int id);
    }
}
