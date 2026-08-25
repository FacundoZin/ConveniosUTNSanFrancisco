using System.ComponentModel.DataAnnotations;
using APIconvenios.Common.Enums;

namespace APIconvenios.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string? Nombre { get; set; }

        public RolUsuario Rol { get; set; } = RolUsuario.Secretario;

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    }
}
