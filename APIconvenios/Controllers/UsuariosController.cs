using APIconvenios.DTOs.Usuario;
using APIconvenios.Interfaces.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIconvenios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> ListarUsuarios()
        {
            var result = await _usuarioService.ListarUsuariosAsync();
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody] InsertUsuarioDto dto)
        {
            var result = await _usuarioService.CrearUsuarioAsync(dto);
            if (!result.Exit)
                return StatusCode(result.Errorcode, result.Errormessage);

            return Ok(result.Data);
        }

        [HttpPut("{id:int}/password")]
        public async Task<IActionResult> CambiarPassword(int id, [FromBody] ResetPasswordDto dto)
        {
            var result = await _usuarioService.CambiarPasswordAsync(id, dto.NewPassword);
            if (!result.Exit)
                return StatusCode(result.Errorcode, result.Errormessage);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var result = await _usuarioService.EliminarUsuarioAsync(id);
            if (!result.Exit)
                return StatusCode(result.Errorcode, result.Errormessage);

            return NoContent();
        }
    }
}
