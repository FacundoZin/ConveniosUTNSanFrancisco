using APIconvenios.DTOs.Empresa;
using APIconvenios.Models;

namespace APIconvenios.Interfaces.Repositorio
{
    public interface IEmpresaRepository
    {
        Task<List<Empresa>> GetAll();
        Task<Empresa> GetById(int id);
        Task<int> Add(Empresa empresa);
        Task<bool> NameEmpresaExist(string Name);
        Task<bool> NameEmpresaExistForUpdate(string Name, int idEmpresa);
        Task<Empresa?> GetEmpresaWithConvenios(int id);
        Task EditEmpresaDto(int idEmpresa, EditEmpresaDto dto);
    }
}
