using APIconvenios.DTOs.ConvenioEspecifico;
using APIconvenios.Models;
using System.Linq.Expressions;

namespace APIconvenios.Interfaces.Repositorio
{
    public interface IConvenioEspecificoReadRepository
    {
        Task<List<ConvenioEspecifico>> ListarConveniosEspecificos(int SaltoPaginas, int CantidadPaginas,
            Expression<Func<ConvenioEspecifico, bool>> filtro,
            Func<IQueryable<ConvenioEspecifico>, IOrderedQueryable<ConvenioEspecifico>>? ordenamiento = null);

        Task<InfoConvenioEspeficoDto> GetConvenioEspecificoCompleto(int id);
    }
}
