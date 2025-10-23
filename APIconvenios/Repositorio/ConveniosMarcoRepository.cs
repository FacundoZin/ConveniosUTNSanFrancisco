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
            var convenio = await _Context.ConveniosMarcos.FirstOrDefaultAsync(c => c.Id == id);
            if (convenio == null) return null;
            return convenio;
        }

        public void Delete(ConvenioMarco convenio)
        {
            _Context.ConveniosMarcos.Remove(convenio);
        }

        public void CreateConvenio(ConvenioMarco convenioMarco)
        {
            _Context.ConveniosMarcos.Add(convenioMarco);
        }

        public void ModificarConvenioMarco(ConvenioMarco convenioMarcoActualizado)
        {
            _Context.ConveniosMarcos.Update(convenioMarcoActualizado);
        }

        public IQueryable<ConvenioMarco> GetQuery()
        {
            return _Context.ConveniosMarcos.AsQueryable();
        }

        public async Task<ConvenioMarco?> GetByNumeroConvenio(string Numero)
        {
            var convenio = await _Context.ConveniosMarcos.FirstOrDefaultAsync(c => c.numeroconvenio == Numero);
            if (convenio == null) return null;
            return convenio;
        }
    }
}
