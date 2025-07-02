using APIconvenios.DTOs.Empresa;
using APIconvenios.Interfaces.Repositorio;
using APIconvenios.Interfaces.Servicios;
using APIconvenios.Models;

namespace APIconvenios.Services
{
    public class ServicioEmpresas : IServicioEmpresas
    {
        private readonly IEmpresaRepository _RepositorioEmpresas;

        public ServicioEmpresas(IEmpresaRepository repository)
        {
            _RepositorioEmpresas = repository;
        }   
        public async Task CargarNuevaEmpresa(CreateEmpresaDto dto)
        {
            await _RepositorioEmpresas.Add(new Empresa
            {
                Nombre = dto.Nombre,
                RazonSocial = dto.RazonSocial,
                Cuit = dto.Cuit,
                Direccion = dto.Direccion,
                Telefono = dto.Telefono,
                Email = dto.Email
            });
        }

        public async Task<Empresa> ObtenerEmpresaPorId(int id)
        {
            return await _RepositorioEmpresas.GetById(id);
        }

        public async Task<List<Empresa>> ObtenerListaEmpresas()
        {
            return await _RepositorioEmpresas.GetAll();
        }
    }
}
