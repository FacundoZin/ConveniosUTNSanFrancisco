using APIconvenios.DTOs.Auth;
using APIconvenios.Interfaces.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace APIconvenios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var usuario = await _authService.LoginAsync(request);
            if (usuario == null)
                return Unauthorized("Credenciales inválidas.");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Username),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };

            if (!string.IsNullOrWhiteSpace(usuario.Nombre))
                claims.Add(new Claim("name", usuario.Nombre));

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return Ok(new LoginResponseDto
            {
                Username = usuario.Username,
                Nombre = usuario.Nombre,
                Rol = usuario.Rol
            });
        }

        [HttpGet("me")]
        public IActionResult Me()
        {
            var session = new LoginResponseDto
            {
                Username = User.Identity?.Name ?? string.Empty,
                Nombre = User.FindFirst("name")?.Value,
                Rol = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty
            };

            return Ok(session);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return NoContent();
        }
    }
}
