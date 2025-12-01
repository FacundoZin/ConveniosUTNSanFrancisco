using APIconvenios.Common;
using APIconvenios.Common.Enums;
using APIconvenios.Data;
using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.Interfaces.Repositorio;
using APIconvenios.Models;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Repositorio
{
    public class ConveniosMarcoRepository : IConvenioMarcoRepository
    {
        private readonly ApplicationDbContext _Context;
        private readonly IDbContextFactory<ApplicationDbContext> _Factory;

        public ConveniosMarcoRepository(ApplicationDbContext context, IDbContextFactory<ApplicationDbContext> factory)
        {
            _Context = context;
            _Factory = factory;
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


        public async Task SetStateToFinish(DateOnly date)
        {
            using var context = _Factory.CreateDbContext();
            await context.ConveniosMarcos.Where(c => c.FechaFin == date)
                .ExecuteUpdateAsync(convenios => convenios.SetProperty(conv => conv.Estado, conv => EstadoConvenio.Finalizado));
        }
    }
}
