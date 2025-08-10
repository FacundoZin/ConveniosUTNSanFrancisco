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

        public async Task<bool> Delete(ConvenioEspecifico convenio)
        {
            _Context.ConveniosEspecificos.Remove(convenio);
            var affected = await _Context.SaveChangesAsync();
            return affected > 0;
        }

        public async Task<ConvenioEspecifico?> GetByid(int id)
        {
            var convenio = await _Context.ConveniosEspecificos.FirstOrDefaultAsync(C => C.Id == id);
            return convenio;
        }

        public async Task<bool> ModificarConvenioEspecifico(ConvenioEspecifico convenio)
        {
            _Context.ConveniosEspecificos.Update(convenio);
            var affected = await _Context.SaveChangesAsync();
            return affected > 0;    
        }
    }
}
