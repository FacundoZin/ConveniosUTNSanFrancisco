using APIconvenios.DTOs.Involucrado;
using APIconvenios.Models;

namespace APIconvenios.Interfaces.Repositorio
{
    public interface IInvolucradosRepository
    {
        Task<bool> involucradoExist(string nombre, string apellido);
        Task< List<Involucrados>> GetAllInvolucraods();

    }
}
