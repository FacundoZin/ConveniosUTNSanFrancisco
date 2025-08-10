using APIconvenios.Models;

namespace APIconvenios.Interfaces.Repositorio
{
    public interface IConvenioEspecificoRepository
    {
        Task<ConvenioEspecifico?> GetByid(int id);
        Task<bool> Delete(ConvenioEspecifico convenio);
        void CreateConvenio(ConvenioEspecifico convenio);
        Task<bool> ModificarConvenioEspecifico(ConvenioEspecifico convenio);
    }
}
