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

        public async Task<InfoConvenioEspeficoDto?> GetConvenioEspecificoCompleto(int id)
        {
            var convenio = await _context.ConveniosEspecificos
                .Include(c => c.empresa)
                .Include(c => c.ConvenioMarco)
                .Include(c => c.ArchivosAdjuntos)
                .Include(c => c.Involucrados)
                .Include(c => c.CarrerasInvolucradas)
                .FirstOrDefaultAsync(c => c.Id == id);

            return convenio != null ? convenio.ToFullInfo() : null;
        }

        public async Task<bool> TitleExist(string title)
        {           
            return await _context.ConveniosEspecificos.AnyAsync(c => c.TituloConvenio == title);
        }

        public async Task<bool> TitleExistForUpdate(string title, int idConvenio)
        {
            return await _context.ConveniosEspecificos.AnyAsync(c => c.TituloConvenio == title && c.Id != idConvenio);
        }
    }
}
