using APIconvenios.DTOs.Empresa;
using APIconvenios.Models;

namespace APIconvenios.Interfaces.Servicios
{
    public interface IServicioEmpresas
    {
        Task<List<EmpresaDto>> ObtenerListaEmpresas();
        Task CargarNuevaEmpresa(CreateEmpresaDto dto);   
    }
}
