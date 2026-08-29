using APIconvenios.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace APIconvenios.Models
{
    public class Involucrados
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string? Email { get; set; }
        [Required(ErrorMessage = "El teléfono es obligatorio")]
        public string Telefono { get; set; } = string.Empty;
        public int? Legajo { get; set; }
        public Roles RolInvolucrado { get; set; }

        public int? IdCarrera { get; set; }
        public Area? Carrera { get; set; }

        public List<ConvenioEspecifico> ConveniosEspecificos { get; set; } = new List<ConvenioEspecifico>();
    }
}
