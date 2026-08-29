using APIconvenios.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace APIconvenios.DTOs.Involucrados
{
    public class InsertInvolucradosDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;
        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Apellido { get; set; } = string.Empty;
        public string? Email { get; set; }
        [Required(ErrorMessage = "El teléfono es obligatorio")]
        public string Telefono { get; set; } = string.Empty;
        public int? Legajo { get; set; }
        public int? IdCarrera { get; set; }
        public Roles RolInvolucrado { get; set; }
    }
}
