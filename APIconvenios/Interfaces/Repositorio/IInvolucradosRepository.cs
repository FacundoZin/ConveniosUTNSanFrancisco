using APIconvenios.Models;

namespace APIconvenios.Interfaces.Repositorio
{
    public interface IInvolucradosRepository
    {
        Task Create(List<Involucrados> involucrados);
        Task<List<Involucrados>> GetInvolucrados(List<int> Ids);
        
    }
}
