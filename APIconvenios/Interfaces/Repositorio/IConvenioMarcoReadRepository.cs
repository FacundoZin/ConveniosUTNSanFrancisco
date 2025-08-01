using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.Models;
using System.Linq.Expressions;

namespace APIconvenios.Interfaces.Repositorio
{
    public interface IConvenioMarcoReadRepository
    {
        Task<List<ConvenioMarco>> GetAllConveniosMarcos(Expression<Func<ConvenioMarco, bool>> filtro,
            Func<IQueryable<ConvenioMarco>, IOrderedQueryable<ConvenioMarco>>? ordenamiento = null);
        Task<InfoConvenioMarcoDto?> GetConvenioMarcosCompleto(int id);
    }
}
