using APIconvenios.DTOs.Auth;

namespace APIconvenios.Interfaces.Servicios
{
    public interface IAuthService
    {
        Task<UsuarioAutenticadoDto?> LoginAsync(LoginRequestDto request);
    }
}
