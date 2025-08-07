using APIconvenios.Models;

namespace APIconvenios.Interfaces.Repositorio
{
    public interface IInvolucradosRepository
    {
        public Task Create(List<Involucrados> involucrados);
    }
}
