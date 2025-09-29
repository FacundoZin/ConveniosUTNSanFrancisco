using APIconvenios.Data;
using APIconvenios.DTOs.ConvenioEspecifico;
using APIconvenios.DTOs.Involucrados;
using APIconvenios.Helpers.Mappers;
using APIconvenios.Interfaces.Repositorio;
using APIconvenios.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace APIconvenios.Repositorio
{
    public class ConvenioEspecificoReadRepository : IConvenioEspecificoReadRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _ContextFactory;
        private readonly ApplicationDbContext _context;

        public ConvenioEspecificoReadRepository(ApplicationDbContext context,
            IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _context = context;
            _ContextFactory = contextFactory;
        }

        public async Task<InfoConvenioEspeficoDto> GetConvenioEspecificoCompleto(int id)
        {
            var convenio = await _context.ConveniosEspecificos.FirstAsync(c => c.Id == id);

            return convenio.ToFullInfo();
        }

        public async Task<List<ConvenioEspecifico>> ListarConveniosEspecificos(int SaltoPaginas, int CantidadPaginas, 
            Expression<Func<ConvenioEspecifico, bool>> filtro, Func<IQueryable<ConvenioEspecifico>, 
                IOrderedQueryable<ConvenioEspecifico>>? ordenamiento = null)
        {
            await using var context = _ContextFactory.CreateDbContext();

            var query = context.ConveniosEspecificos.Where(filtro);

            if (ordenamiento != null)
                query = ordenamiento(query);

            query.Skip(SaltoPaginas);
            query.Take(CantidadPaginas);

            return await query.ToListAsync();
        }

        public async Task<bool> TitleExist(string title)
        {           
            return await _context.ConveniosEspecificos.AnyAsync(c => c.Titulo == title);
        }

        public async Task<bool> TitleExistForUpdate(string title, int idConvenio)
        {
            return await _context.ConveniosEspecificos.AnyAsync(c => c.Titulo == title && c.Id != idConvenio);
        }
    }
}
