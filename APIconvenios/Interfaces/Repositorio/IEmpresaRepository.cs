using APIconvenios.Models;

namespace APIconvenios.Interfaces.Repositorio
{
    public interface IEmpresaRepository
    {
        Task<List<Empresa>> GetAll();
        Task<Empresa> GetById(int id);
        void Add(Empresa empresa);
    }
}
