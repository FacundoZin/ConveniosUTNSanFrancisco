using APIconvenios.DTOs.Empresa;
using APIconvenios.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace APIconvenios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly _UnitOfWork _UnitOfWork;
        public EmpresaController(_UnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> ListarEmpresas()
        {
            var empresas = await _UnitOfWork._EmpresaRepository.GetAll();

            var dto = empresas.Select(e => new ComboBoxEmpresasDto
            {
                IdEmpresa = e.Id,
                NombreEmpresa = e.Nombre
            });

            return Ok(dto);
        }

        [HttpPut("{idEmpresa:int}")]
        public async Task<IActionResult> EditarInfoEmpresa([FromQuery] int idEmpresa, [FromBody] EditEmpresaDto dto)
        {
            await _UnitOfWork._EmpresaRepository.EditEmpresaDto(idEmpresa, dto);

            return NoContent();
        }
    }
}