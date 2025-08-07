using APIconvenios.Enums;
using System.ComponentModel.DataAnnotations;

namespace APIconvenios.DTOs.Involucrados
{
    public class CreateInvolucradosDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(50, ErrorMessage = "El apellido no puede superar los 50 caracteres.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El teléfono debe tener 10 dígitos numéricos.")]
        public string Telefono { get; set; }

        public int Legajo { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio.")]
        [EnumDataType(typeof(Roles), ErrorMessage = "Rol inválido.")]
        public Roles RolInvolucrado { get; set; }
    }
}
