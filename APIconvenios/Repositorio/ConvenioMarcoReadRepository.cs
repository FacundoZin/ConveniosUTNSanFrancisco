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

        public async Task<InfoConvenioMarcoDto?> GetConvenioMarcosCompleto(int id)
        {
            var convenio = await _Context.ConveniosMarcos.FirstAsync(c => c.Id == id);

            return convenio.ToFullInfo();
        }

        public async Task<bool> TitleExist(string Title)
        {
            return await _Context.ConveniosMarcos.AnyAsync(c => c.Titulo.ToLower() == Title.ToLower());
        }

        public async Task<bool> TitleExistForUpdate(string Title, int idConvenio)
        {
            return await _Context.ConveniosMarcos
                .AnyAsync(c => c.Titulo.ToLower() == Title.ToLower() && c.Id != idConvenio);
        }
    }
}
