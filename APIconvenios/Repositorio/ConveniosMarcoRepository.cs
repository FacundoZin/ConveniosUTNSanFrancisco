using APIconvenios.Common;
using APIconvenios.Data;
using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.Interfaces.Repositorio;
using APIconvenios.Models;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Repositorio
{
    public class ConveniosMarcoRepository : IConvenioMarcoRepository
    {
        public ApplicationDbContext _Context { get; set; }

        public ConveniosMarcoRepository(ApplicationDbContext context)
        {
            _Context = context;
        }

        public async Task<ConvenioMarco?> GetByid(int id)
        {
            var convenio = await _Context.ConveniosMarcos.FirstAsync(c => c.Id == id);
            if (convenio == null) return null;
            return convenio;
        }

        public async Task<bool> Delete(ConvenioMarco convenio)
        {
            _Context.ConveniosMarcos.Remove(convenio);
            var affected = await _Context.SaveChangesAsync();
            return affected > 0;
        }

        public void CreateConvenio(ConvenioMarco convenioMarco)
        {
            _Context.ConveniosMarcos.Add(convenioMarco);  
        }

        public async Task<bool> ModificarConvenioMarco(ConvenioMarco convenioMarcoActualizado)
        {
            _Context.ConveniosMarcos.Update(convenioMarcoActualizado);  
            int rowsaffected = await _Context.SaveChangesAsync();
            return rowsaffected > 0;
        }
    }
}
