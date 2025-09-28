using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.Models;

namespace APIconvenios.Interfaces.Repositorio
{
    public interface IConvenioMarcoRepository
    {
        Task<ConvenioMarco?> GetByid(int id);
        Task<bool> Delete(ConvenioMarco convenio);
        void CreateConvenio(ConvenioMarco convenioMarco);
        Task<bool> ModificarConvenioMarco(ConvenioMarco convenioMarcoActualizado);
        IQueryable<ConvenioMarco> GetQuery();
    }
}
