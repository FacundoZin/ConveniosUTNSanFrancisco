using APIconvenios.Models;

namespace APIconvenios.Interfaces.Repositorio
{
    public interface ICarreraRepository
    {
        public Task<List<Carreras>> GetCarrerasByID(int[] ids);
    }
}
