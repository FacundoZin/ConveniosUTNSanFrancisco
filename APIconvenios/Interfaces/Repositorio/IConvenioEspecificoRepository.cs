using APIconvenios.Models;

namespace APIconvenios.Interfaces.Repositorio
{
    public interface IConvenioEspecificoRepository
    {
        Task<ConvenioEspecifico?> GetByid(int id);
        Task<List<ConvenioEspecifico>> GetConveniosByIds(int[] Ids);
        Task<bool> Delete(ConvenioEspecifico convenio);
        void CreateConvenio(ConvenioEspecifico convenio);
        Task<bool> ModificarConvenioEspecifico(ConvenioEspecifico convenio);
        IQueryable<ConvenioEspecifico> GetQuery();
    }
}
