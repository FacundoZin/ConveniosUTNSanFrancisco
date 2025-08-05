using APIconvenios.Models;

namespace APIconvenios.Interfaces.Repositorio
{
    public interface IConvenioEspecificoRepository
    {
        Task<ConvenioEspecifico?> GetByid(int id);
        Task<bool> Delete(ConvenioEspecifico convenio);
        Task<bool> CreateConvenio(ConvenioEspecifico convenio);
        Task<bool> ModificarConvenioMarco(ConvenioEspecifico convenio);
    }
}
