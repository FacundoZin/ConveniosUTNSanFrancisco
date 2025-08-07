using System.ComponentModel.DataAnnotations;

namespace APIconvenios.Models
{
    public class Empresa
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La razón social es obligatoria.")]
        [StringLength(150, ErrorMessage = "La razón social no puede superar los 150 caracteres.")]
        public string RazonSocial { get; set; }

        [Required(ErrorMessage = "El CUIT es obligatorio.")]
        [Range(20000000000, 27999999999, ErrorMessage = "CUIT inválido.")]
        public long Cuit { get; set; } // Ojo: cambié a `long` porque un CUIT puede superar `int.MaxValue`.

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [StringLength(200, ErrorMessage = "La dirección no puede superar los 200 caracteres.")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El número de teléfono debe tener exactamente 10 dígitos(sin caracteristica de pais, ni espacios).")]
        public string Telefono { get; set; } // Cambié a string por formato y validación más flexible.

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; }
    }
}
