using APIconvenios.Common.Enums;
using APIconvenios.Data;
using APIconvenios.Interfaces.Repositorio;
using APIconvenios.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace APIconvenios.Repositorio
{
    public class ConvenioEspecificoRepository : IConvenioEspecificoRepository
    {
        private readonly ApplicationDbContext _Context;
        private readonly IDbContextFactory<ApplicationDbContext> _Factory;
        public ConvenioEspecificoRepository(ApplicationDbContext context, IDbContextFactory<ApplicationDbContext> factory)
        {
            _Context = context;
            _Factory = factory;
        }

        public void CreateConvenio(ConvenioEspecifico convenio)
        {
            _Context.ConveniosEspecificos.Add(convenio);
        }

        public void Delete(ConvenioEspecifico convenio)
        {
            _Context.ConveniosEspecificos.Remove(convenio);
        }

        public async Task<ConvenioEspecifico?> GetByid(int id)
        {
            var convenio = await _Context.ConveniosEspecificos.FirstOrDefaultAsync(C => C.Id == id);
            return convenio;
        }

        public async Task<ConvenioEspecifico?> GetByNumeroConvenio(string numero)
        {
            var convenio = await _Context.ConveniosEspecificos.FirstOrDefaultAsync(c => c.numeroconvenio == numero);
            return convenio;
        }

        public async Task<List<ConvenioEspecifico>> GetConveniosByIds(int[] Ids)
        {
            return await _Context.ConveniosEspecificos.Where(c => Ids.Contains(c.Id)).ToListAsync();
        }

        public IQueryable<ConvenioEspecifico> GetQuery()
        {
            return _Context.ConveniosEspecificos.AsQueryable();
        }

        public void ModificarConvenioEspecifico(ConvenioEspecifico convenio)
        {
            _Context.ConveniosEspecificos.Update(convenio);
        }

        public async Task SetStateTofinish(DateOnly finishDate)
        {
            using var context = _Factory.CreateDbContext();
            await context.ConveniosEspecificos.Where(c => c.FechaFinConvenio == finishDate)
                .ExecuteUpdateAsync(convenios => convenios.SetProperty(conv => conv.Estado, conv => EstadoConvenio.Finalizado));
        }
    }
}
