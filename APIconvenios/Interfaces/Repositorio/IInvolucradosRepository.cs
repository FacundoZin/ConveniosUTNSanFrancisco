using APIconvenios.DTOs.Involucrado;
using APIconvenios.Models;

namespace APIconvenios.Interfaces.Repositorio
{
    public interface IInvolucradosRepository
    {
        Task<bool> involucradoExist(string nombre, string apellido);
        Task< List<Involucrados>> GetAllInvolucraods();
        Task<List<Involucrados>> GetInvolucradosByIds(int[] ids);
        Task<List<Involucrados>> GetInvolucradosByCarrera(int IdCarrera);
        Task<List<Involucrados>> GetAvailableForConvenio(int idConvenio);
    }
}
