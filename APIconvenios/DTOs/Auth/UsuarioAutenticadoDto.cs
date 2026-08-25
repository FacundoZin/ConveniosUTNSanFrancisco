namespace APIconvenios.DTOs.Auth
{
    /// <summary>
    /// Resultado interno del login (incluye el Id para los claims).
    /// No se expone directamente en la respuesta HTTP.
    /// </summary>
    public class UsuarioAutenticadoDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? Nombre { get; set; }
        public string Rol { get; set; }
    }
}
