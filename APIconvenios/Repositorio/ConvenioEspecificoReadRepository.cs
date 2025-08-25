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
            var convenio = await _context.ConveniosEspecificos.Where(c => c.Id == id).Select(c => new InfoConvenioEspeficoDto
            {
                Id = c.Id,
                ConvenioMarcoId = c.ConvenioMarcoId,
                numeroconvenio = c.ConvenioMarco.numeroconvenio,
                Titulo = c.Titulo,
                FechaFirmaConvenio = c.FechaFirmaConvenio,
                FechaFinConvenio = c.FechaFinConvenio,
                FechaInicioActividades = c.FechaInicioActividades,
                ComentarioOpcional = c.ComentarioOpcional,
                Involucrados = c.Involucrados.Select(i => new InvolucradosDto
                {
                    Id = i.Id,
                    Nombre = i.Nombre,
                    Apellido = i.Apellido,
                    Email = i.Email,
                    Telefono = i.Telefono,
                    Legajo = i.Legajo,
                    RolInvolucrado = i.RolInvolucrado.ToString(),
                }).ToList()
            }).AsNoTracking().FirstAsync();

            return convenio;
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
