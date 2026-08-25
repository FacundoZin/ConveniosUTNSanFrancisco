using System.ComponentModel.DataAnnotations;

namespace APIconvenios.DTOs.Usuario
{
    public class InsertUsuarioDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Rol { get; set; }
    }
}
