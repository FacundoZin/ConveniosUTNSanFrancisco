using APIconvenios.Data;
using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.Interfaces.Repositorio;
using APIconvenios.Models;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Repositorio
{
    public class ConveniosRepository : IConvenioMarcoRepository
    {
        public ApplicationDbContext _Context { get; set; }

        public ConveniosRepository(ApplicationDbContext context)
        {
            _Context = context;
        }

        public async Task<ConvenioMarco> GetByid(int id)
        {
            return await _Context.ConveniosMarcos.FirstAsync(c => c.Id == id);
        }

        public async Task<bool> Delete(int id)
        {
            var convenio = await _Context.ConveniosMarcos.FindAsync(id);
            if (convenio == null)
                return false; // o throw new NotFoundException();

            _Context.ConveniosMarcos.Remove(convenio);
            var affected = await _Context.SaveChangesAsync();
            return affected > 0;
        }

        public async Task CreateConvenio(ConvenioMarco convenioMarco)
        {
            await _Context.ConveniosMarcos.AddAsync(convenioMarco);
            await _Context.SaveChangesAsync();  
        }

        public async Task ModificarConvenioMarco(ConvenioMarco convenioMarcoActualizado)
        {
            _Context.ConveniosMarcos.Update(convenioMarcoActualizado);  
            await _Context.SaveChangesAsync();
        }
    }
}
