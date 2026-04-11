using APIconvenios.DTOs.Empresa;
using APIconvenios.Models;
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
                NombreEmpresa = e.Nombre,
            });

            return Ok(dto);
        }

        [HttpPut("{idEmpresa:int}")]
        public async Task<IActionResult> EditarInfoEmpresa(int idEmpresa, [FromBody] EditEmpresaDto dto)
        {
            await _UnitOfWork._EmpresaRepository.EditEmpresaDto(idEmpresa, dto);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CrearEmpresa([FromBody] InsertEmpresaDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                return BadRequest("El nombre de la empresa es requerido.");

            var exists = await _UnitOfWork._EmpresaRepository.NameEmpresaExist(dto.Nombre);
            if (!exists.Exit)
            {
                return BadRequest(exists.Errormessage);
            }

            var nuevaEmpresa = new Empresa
            {
                Nombre = dto.Nombre,
                RazonSocial = dto.RazonSocial,
                Cuit = dto.Cuit,
                Direccion = dto.Direccion,
                Telefono = dto.Telefono,
                Email = dto.Email
            };

            await _UnitOfWork._EmpresaRepository.Add(nuevaEmpresa);
            await _UnitOfWork.Save();

            return Ok(nuevaEmpresa.Id);
        }
    }
}