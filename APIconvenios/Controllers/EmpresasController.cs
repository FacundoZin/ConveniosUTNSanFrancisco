using APIconvenios.DTOs.Empresa;
using APIconvenios.Interfaces.Servicios;
using APIconvenios.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIconvenios.Controllers
{
    [Route("api/Empresas")]
    [ApiController]
    public class EmpresasController : ControllerBase
    {
        private readonly IServicioEmpresas _ServicioEmpresas;
        public EmpresasController(IServicioEmpresas servicioempresas)
        {
            _ServicioEmpresas = servicioempresas;
        }

        [HttpGet]
        public async Task<IActionResult> ListaEmpresas()
        {
            var empresas = await _ServicioEmpresas.ObtenerListaEmpresas();
            return Ok(empresas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DatosEmpresa(int id)
        {
            var empresa = await _ServicioEmpresas.ObtenerEmpresaPorId(id);
            return Ok(empresa);
        }

        [HttpPost]
        public async Task CargarNuevaEmpresa([FromBody] CreateEmpresaDto dto)
        {
            await _ServicioEmpresas.CargarNuevaEmpresa(dto);
        }
    }
}
