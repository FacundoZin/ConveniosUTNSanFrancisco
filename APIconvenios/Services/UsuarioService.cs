using APIconvenios.Common;
using APIconvenios.Common.Enums;
using APIconvenios.Data;
using APIconvenios.DTOs.Usuario;
using APIconvenios.Interfaces.Servicios;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private const int MinimoCaracteresPassword = 8;

        public UsuarioService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Result<List<UsuarioDto>>> ListarUsuariosAsync()
        {
            await using var ctx = await _contextFactory.CreateDbContextAsync();

            var usuarios = await ctx.Usuarios
                .AsNoTracking()
                .Select(u => new UsuarioDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Nombre = u.Nombre,
                    Rol = u.Rol.ToString()
                })
                .ToListAsync();

            return Result<List<UsuarioDto>>.Exito(usuarios);
        }

        public async Task<Result<UsuarioDto>> CrearUsuarioAsync(InsertUsuarioDto dto)
        {
            var errorValidacion = ValidarCampos(dto.Username, dto.Password, dto.Nombre);
            if (errorValidacion != null)
                return Result<UsuarioDto>.Error(errorValidacion, 400);

            if (!Enum.TryParse<RolUsuario>(dto.Rol, ignoreCase: true, out var rol))
                return Result<UsuarioDto>.Error("El rol especificado no es válido.", 400);

            await using var ctx = await _contextFactory.CreateDbContextAsync();

            var usernameExistente = await ctx.Usuarios
                .AnyAsync(u => u.Username == dto.Username);

            if (usernameExistente)
                return Result<UsuarioDto>.Error("El nombre de usuario ya está en uso.", 409);

            var usuario = new Models.Usuario
            {
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Nombre = dto.Nombre,
                Rol = rol,
                FechaCreacion = DateTime.UtcNow
            };

            ctx.Usuarios.Add(usuario);
            await ctx.SaveChangesAsync();

            return Result<UsuarioDto>.Exito(new UsuarioDto
            {
                Id = usuario.Id,
                Username = usuario.Username,
                Nombre = usuario.Nombre,
                Rol = usuario.Rol.ToString()
            });
        }

        public async Task<Result<bool>> CambiarPasswordAsync(int id, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < MinimoCaracteresPassword)
                return Result<bool>.Error($"La contraseña debe tener al menos {MinimoCaracteresPassword} caracteres.", 400);

            await using var ctx = await _contextFactory.CreateDbContextAsync();

            var usuario = await ctx.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario == null)
                return Result<bool>.Error("No existe un usuario con el id indicado.", 404);

            usuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await ctx.SaveChangesAsync();

            return Result<bool>.Exito(true);
        }

        public async Task<Result<bool>> EliminarUsuarioAsync(int id)
        {
            await using var ctx = await _contextFactory.CreateDbContextAsync();

            var usuario = await ctx.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario == null)
                return Result<bool>.Error("No existe un usuario con el id indicado.", 404);

            if (usuario.Rol == RolUsuario.Administrador)
            {
                var hayOtrosAdministradores = await ctx.Usuarios
                    .AnyAsync(u => u.Id != id && u.Rol == RolUsuario.Administrador);

                if (!hayOtrosAdministradores)
                    return Result<bool>.Error("No se puede eliminar al último administrador restante.", 400);
            }

            ctx.Usuarios.Remove(usuario);
            await ctx.SaveChangesAsync();

            return Result<bool>.Exito(true);
        }

        private static string? ValidarCampos(string? username, string? password, string? nombre)
        {
            if (string.IsNullOrWhiteSpace(username))
                return "El campo Username es obligatorio.";

            if (string.IsNullOrWhiteSpace(password) || password.Length < MinimoCaracteresPassword)
                return $"La contraseña debe tener al menos {MinimoCaracteresPassword} caracteres.";

            if (string.IsNullOrWhiteSpace(nombre))
                return "El campo Nombre es obligatorio.";

            return null;
        }
    }
}
