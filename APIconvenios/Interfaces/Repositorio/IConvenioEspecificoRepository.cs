using APIconvenios.Models;

namespace APIconvenios.Interfaces.Repositorio
{
    public interface IConvenioEspecificoRepository
    {
        Task<ConvenioEspecifico?> GetByid(int id);
        Task<List<ConvenioEspecifico>> GetConveniosByIds(int[] Ids);
        void Delete(ConvenioEspecifico convenio);
        void CreateConvenio(ConvenioEspecifico convenio);
        void ModificarConvenioEspecifico(ConvenioEspecifico convenio);
        IQueryable<ConvenioEspecifico> GetQueryByFiltering();

        Task SetStateTofinish(DateOnly finishDate);

        Task<ConvenioEspecifico?> GetByNumeroConvenio(string numero);
    }
}
