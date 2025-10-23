using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.Models;

namespace APIconvenios.Interfaces.Repositorio
{
    public interface IConvenioMarcoRepository
    {
        Task<ConvenioMarco?> GetByid(int id);
        void Delete(ConvenioMarco convenio);
        void CreateConvenio(ConvenioMarco convenioMarco);
        void ModificarConvenioMarco(ConvenioMarco convenioMarcoActualizado);
        IQueryable<ConvenioMarco> GetQuery();

        Task<ConvenioMarco?> GetByNumeroConvenio(string Numero);
    }
}
