using APIconvenios.Common;
using APIconvenios.DTOs.Usuario;

namespace APIconvenios.Interfaces.Servicios
{
    public interface IUsuarioService
    {
        Task<Result<List<UsuarioDto>>> ListarUsuariosAsync();
        Task<Result<UsuarioDto>> CrearUsuarioAsync(InsertUsuarioDto dto);
        Task<Result<bool>> CambiarPasswordAsync(int id, string newPassword);
        Task<Result<bool>> EliminarUsuarioAsync(int id);
    }
}
