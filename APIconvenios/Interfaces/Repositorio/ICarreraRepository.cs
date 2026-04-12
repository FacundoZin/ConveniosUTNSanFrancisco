using APIconvenios.Models;

namespace APIconvenios.Interfaces.Repositorio
{
    public interface ICarreraRepository
    {
        public Task<List<Area>> GetCarrerasByID(int[] ids);
    }
}
