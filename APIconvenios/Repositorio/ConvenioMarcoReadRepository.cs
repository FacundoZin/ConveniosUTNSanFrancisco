using APIconvenios.Common;
using APIconvenios.Data;
using APIconvenios.DTOs.ConvenioEspecifico;
using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.DTOs.Empresa;
using APIconvenios.Helpers.Mappers;
using APIconvenios.Interfaces.Repositorio;
using APIconvenios.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace APIconvenios.Repositorio
{
    public class ConvenioMarcoReadRepository : IConvenioMarcoReadRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _ContextFactory;
        private readonly ApplicationDbContext _Context;

        public ConvenioMarcoReadRepository(ApplicationDbContext context,
            IDbContextFactory<ApplicationDbContext> contextfactory)
        {
            _Context = context;
            _ContextFactory = contextfactory;
        }

        public async Task<List<ComboBoxConvenioMarcoDto>> GetAllWithoutTracking()
        {
            var convenios = await _Context.ConveniosMarcos.AsNoTracking()
                .Select(cm => new ComboBoxConvenioMarcoDto { Id = cm.Id, Titulo = cm.Titulo }).ToListAsync();

            if(convenios == null || convenios.Count == 0) return new List<ComboBoxConvenioMarcoDto>();

            return convenios;
        }

        public async Task<ConvenioMarco?> GetByidWithConvEspecifico(int id)
        {
            var convenio = await _Context.ConveniosMarcos.Include(c => c.ConveniosEspecificos)
            .FirstOrDefaultAsync(c => c.Id == id);
            if (convenio == null) return null;
            return convenio;
        }

        public async Task<InfoConvenioMarcoDto?> GetConvenioMarcosCompleto(int id)
        {
            var convenio = await _Context.ConveniosMarcos
                .Include(cm => cm.Empresa)
                .Include(cm => cm.ConveniosEspecificos)
                .Include(cm => cm.ArchivosAdjuntos)
                .FirstOrDefaultAsync(cm => cm.Id == id);


            return convenio != null ? convenio.ToFullInfo() : null;
        }

        public async Task<Result<object?>> TitleConvenioExist(string Title)
        {
            var context = _ContextFactory.CreateDbContext();
            bool Exist = await context.ConveniosMarcos.AnyAsync(c => c.Titulo.ToLower() == Title.ToLower());

            if (Exist) return Result<object?>.Error("Ya existe un convenio marco con ese titulo", 400);

            return Result<object?>.Exito(null);
        }

        public async Task<Result<object?>> TitleConvenioExistForUpdate(string title, int id)
        {
            var context = _ContextFactory.CreateDbContext();
            bool Exist = await context.ConveniosMarcos.AnyAsync(c => c.Titulo.ToLower() == title.ToLower() && c.Id != id);

            if (Exist) return Result<object?>.Error("Ya existe un convenio marco con ese titulo", 400);

            return Result<object?>.Exito(null);
        }

        public async Task<Result<object?>> NumeroConvenioExist(string numeroConvenio)
        {
            var context = _ContextFactory.CreateDbContext();
            bool Exist = await context.ConveniosMarcos.AnyAsync(c => c.numeroconvenio == numeroConvenio);

            if (Exist) return Result<object?>.Error("Ya existe un convenio marco con ese numero", 400);

            return Result<object?>.Exito(null);
        }

        public async Task<Result<object?>> NumeroConvenioExistForUpdate(string numeroConvenio, int id)
        {
            var context = _ContextFactory.CreateDbContext();
            bool Exist = await context.ConveniosEspecificos.AnyAsync(c => c.numeroconvenio == numeroConvenio && c.Id != id);

            if (Exist) return Result<object?>.Error("Ya existe un convenio marco con ese numero", 400);

            return Result<object?>.Exito(null);
        }

    }
}
