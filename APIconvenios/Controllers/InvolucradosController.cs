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

            return Ok(existe);
        }
    }
}
