using APIconvenios.DTOs.Involucrado;
using APIconvenios.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIconvenios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvolucradosController : ControllerBase
    {
        private readonly _UnitOfWork _UnitOfWork;

        public InvolucradosController(_UnitOfWork unitOfWork) 
        {
            _UnitOfWork = unitOfWork;
        }

        [HttpPost("validate")] 
        public async Task<IActionResult> ValidateInvolucrado([FromBody] ValidateInvolucradoDto _dto)
        {
            if (_dto == null || string.IsNullOrWhiteSpace(_dto.nombre) || string.IsNullOrWhiteSpace(_dto.apellido))
            {
                return BadRequest("el involucrado debe tener nombre y apellido para validarlo");
            }

            var existe = await _UnitOfWork._InvolucradosRepository.involucradoExist(_dto.nombre, _dto.apellido);

            if(existe)
                return Ok(new InvolucradoExistDto { existe= true, message = "el involucrado que quiere agregar ya existe"});

            return Ok(new InvolucradoExistDto { existe = false, message = "-" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInvolucrados()
        {
            var involucrados = await _UnitOfWork._InvolucradosRepository.GetAllInvolucraods();

            var dto = involucrados.Select(i => new ComboBoxInvolucradosDto
            {
                Id = i.Id,
                FullName = $"{i.Nombre} {i.Apellido}",
            }).ToList();

            return Ok(dto);
        }
    }
}
