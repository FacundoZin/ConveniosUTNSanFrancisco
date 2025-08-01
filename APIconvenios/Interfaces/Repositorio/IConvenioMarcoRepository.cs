using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.Models;

namespace APIconvenios.Interfaces.Repositorio
{
    public interface IConvenioMarcoRepository
    {
        Task<ConvenioMarco> GetByid(int id);
        Task<bool> Delete(int id);
        Task CreateConvenio(ConvenioMarco convenioMarco);
        Task ModificarConvenioMarco(ConvenioMarco convenioMarcoActualizado);
    }
}
