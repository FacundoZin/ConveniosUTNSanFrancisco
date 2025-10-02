using APIconvenios.Data;
using APIconvenios.Interfaces.Repositorio;
using APIconvenios.Models;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Repositorio
{
    public class ConvenioEspecificoRepository : IConvenioEspecificoRepository
    {
        private readonly ApplicationDbContext _Context;
        public ConvenioEspecificoRepository(ApplicationDbContext context)
        {
            _Context = context;
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
    }
}
