using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.Models;
using System.Linq.Expressions;

namespace APIconvenios.Interfaces.Repositorio
{
    public interface IConvenioMarcoReadRepository
    {
        Task<List<ConvenioMarco>> GetAllConveniosMarcos(int SaltoPaginas, int CantidadPaginas,
            Expression<Func<ConvenioEspecifico, bool>> filtro,
            Func<IQueryable<ConvenioEspecifico>, IOrderedQueryable<ConvenioEspecifico>>? ordenamiento = null);
        Task<InfoConvenioMarcoDto?> GetConvenioMarcosCompleto(int id);
        Task<bool> TitleExist(string Title);
    }
}
