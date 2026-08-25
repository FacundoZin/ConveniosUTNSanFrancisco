using APIconvenios.Data;
using APIconvenios.DTOs.Auth;
using APIconvenios.Interfaces.Servicios;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Services
{
    public class AuthService : IAuthService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public AuthService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<UsuarioAutenticadoDto?> LoginAsync(LoginRequestDto request)
        {
            await using var ctx = await _contextFactory.CreateDbContextAsync();

            var usuario = await ctx.Usuarios
                .FirstOrDefaultAsync(u => u.Username == request.Username);

            if (usuario == null)
                return null;

            if (!BCrypt.Net.BCrypt.Verify(request.Password, usuario.PasswordHash))
                return null;

            return new UsuarioAutenticadoDto
            {
                Id = usuario.Id,
                Username = usuario.Username,
                Nombre = usuario.Nombre,
                Rol = usuario.Rol.ToString()
            };
        }
    }
}
