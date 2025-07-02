using APIconvenios.Models;

namespace APIconvenios.Interfaces.Repositorio
{
    public interface IEmpresaRepository
    {
        Task<List<Empresa>> GetAll();
        Task<Empresa> GetById(int id);
        Task Add(Empresa empresa);
    }
}
