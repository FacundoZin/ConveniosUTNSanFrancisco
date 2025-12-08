using APIconvenios.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace APIconvenios.DTOs.Involucrados
{
    public class InsertInvolucradosDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public int? Legajo { get; set; }
        public int? IdCarrera { get; set; }
        public Roles RolInvolucrado { get; set; }
    }
}
