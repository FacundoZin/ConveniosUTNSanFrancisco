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
        public async Task<bool> CreateConvenio(ConvenioEspecifico convenio)
        {
            await _Context.ConveniosEspecificos.AddAsync(convenio);
            int affected = await _Context.SaveChangesAsync();
            return affected > 0;
        }

        public async Task<bool> Delete(ConvenioEspecifico convenio)
        {
            _Context.ConveniosEspecificos.Remove(convenio);
            var affected = await _Context.SaveChangesAsync();
            return affected > 0;
        }

        public async Task<ConvenioEspecifico?> GetByid(int id)
        {
            return await _Context.ConveniosEspecificos.FirstOrDefaultAsync(C => C.Id == id);
        }

        public async Task<bool> ModificarConvenioMarco(ConvenioEspecifico convenio)
        {
            _Context.ConveniosEspecificos.Update(convenio);
            int rowsaffected = await _Context.SaveChangesAsync();
            return rowsaffected > 0;
        }
    }
}
