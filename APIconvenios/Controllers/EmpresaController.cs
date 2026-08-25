using APIconvenios.DTOs.Empresa;
using APIconvenios.Interfaces.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIconvenios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Secretario")]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;

        public EmpresaController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        [HttpGet]
        public async Task<IActionResult> ListarEmpresasPaginado([FromQuery] int pagina = 1, [FromQuery] int cantidad = 10)
        {
            var result = await _empresaService.ListarEmpresasPaginado(pagina, cantidad);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> ListarTodasLasEmpresas()
        {
            var result = await _empresaService.ListarTodasLasEmpresas();
            return Ok(result.Data);
        }

        [HttpPut("{idEmpresa:int}")]
        public async Task<IActionResult> EditarInfoEmpresa(int idEmpresa, [FromBody] EditEmpresaDto dto)
        {
            var result = await _empresaService.EditarEmpresa(idEmpresa, dto);
            if (!result.Exit) return BadRequest(result.Errormessage);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CrearEmpresa([FromBody] InsertEmpresaDto dto)
        {
            var result = await _empresaService.CrearEmpresa(dto);
            if (!result.Exit) return BadRequest(result.Errormessage);
            return Ok(result.Data);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerEmpresaPorId(int id)
        {
            var result = await _empresaService.ObtenerEmpresaPorId(id);
            if (!result.Exit) return NotFound(result.Errormessage);
            return Ok(result.Data);
        }
    }
}