using APIconvenios.Common;
using APIconvenios.DTOs.Empresa;
using APIconvenios.Models;

namespace APIconvenios.Interfaces.Repositorio
{
    public interface IEmpresaRepository
    {
        Task<List<Empresa>> GetAll();
        Task<Empresa> GetById(int id);
        Task<int> Add(Empresa empresa);
        Task<Result<object?>> NameEmpresaExist(string Name);
        Task<Result<object?>> NameEmpresaExistForUpdate(string Name, int idEmpresa);
        Task<Empresa?> GetEmpresaWithConvenios(int id);
        Task EditEmpresaDto(int idEmpresa, EditEmpresaDto dto);
    }
}
