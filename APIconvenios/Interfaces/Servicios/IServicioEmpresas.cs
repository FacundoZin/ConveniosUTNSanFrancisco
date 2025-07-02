using APIconvenios.DTOs.Empresa;
using APIconvenios.Models;

namespace APIconvenios.Interfaces.Servicios
{
    public interface IServicioEmpresas
    {
        Task<List<Empresa>> ObtenerListaEmpresas();
        Task<Empresa> ObtenerEmpresaPorId(int id);
        Task CargarNuevaEmpresa(CreateEmpresaDto dto);   
    }
}
