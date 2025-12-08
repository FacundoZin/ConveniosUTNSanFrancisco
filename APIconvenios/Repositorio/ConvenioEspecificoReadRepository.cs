using APIconvenios.Common;
using APIconvenios.Data;
using APIconvenios.DTOs.ConvenioEspecifico;
using APIconvenios.Helpers.Mappers;
using APIconvenios.Interfaces.Repositorio;
using APIconvenios.Models;
using Microsoft.EntityFrameworkCore;


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

        public async Task<Result<object?>> TitleConvenioExist(string title)
        {
            var context = _ContextFactory.CreateDbContext();
            bool Exist = await context.ConveniosEspecificos.AnyAsync(c => c.TituloConvenio.ToLower() == title.ToLower());

            if(Exist) return Result<object?>.Error("Ya existe un convenio especifico con ese titulo", 400);

            return Result<object?>.Exito(null);
        }

        public async Task<Result<object?>> NumeroConvenioExist(string numeroConvenio)
        {
            var context = _ContextFactory.CreateDbContext();
            bool Exist = await context.ConveniosEspecificos.AnyAsync(c => c.numeroconvenio == numeroConvenio);

            if (Exist) return Result<object?>.Error("Ya existe un convenio especifico con ese numero", 400);

            return Result<object?>.Exito(null);
        }

        public async Task<Result<object?>> TitleConvenioExistForUpdate(string title, int id)
        {
            var context = _ContextFactory.CreateDbContext();
            bool Exist = await context.ConveniosEspecificos.AnyAsync(c => c.TituloConvenio.ToLower() == title.ToLower() && c.Id != id);

            if (Exist) return Result<object?>.Error("Ya existe un convenio especifico con ese titulo", 400);

            return Result<object?>.Exito(null);
        }

        public async Task<Result<object?>> NumeroConvenioExistForUpdate(string numeroConvenio, int id)
        {
            var context = _ContextFactory.CreateDbContext();
            bool Exist = await context.ConveniosEspecificos.AnyAsync(c => c.numeroconvenio == numeroConvenio && c.Id != id);

            if (Exist) return Result<object?>.Error("Ya existe un convenio especifico con ese numero", 400);

            return Result<object?>.Exito(null);
        }

        public async Task<List<ComboBoxConvenioEspecificoDto>> GetAllWithoutTracking()
        {
            var convenios =  await _context.ConveniosMarcos.AsNoTracking()
                .Select(ce => new ComboBoxConvenioEspecificoDto { Id = ce.Id, Titulo = ce.Titulo}).ToListAsync();

            if (convenios == null || convenios.Count == 0) return new List<ComboBoxConvenioEspecificoDto>();

            return convenios;
        }

        public async Task<InfoConvenioEspeficoDto?> GetConvenioEspecificoCompleto(int id)
        {
            var convenio = await _context.ConveniosEspecificos
                .Include(c => c.empresa)
                .Include(c => c.ConvenioMarco)
                .Include(c => c.ArchivosAdjuntos)
                .Include(c => c.Involucrados!).ThenInclude(i => i.Carrera)
                .Include(c => c.CarrerasInvolucradas)
                .FirstOrDefaultAsync(c => c.Id == id);

            return convenio != null ? convenio.ToFullInfo() : null;
        }

        public async Task<ConvenioEspecifico?> GetConvenioWithRelations(int id)
        {
            var convenio = await _context.ConveniosEspecificos
            .Include(c => c.empresa)
            .Include(c => c.ConvenioMarco)
            .Include(c => c.ArchivosAdjuntos)
            .Include(c => c.Involucrados)
            .Include(c => c.CarrerasInvolucradas)
            .FirstOrDefaultAsync(c => c.Id == id);

            return convenio != null ? convenio : null;
        }
    }
}
