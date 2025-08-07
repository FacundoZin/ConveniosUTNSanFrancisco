using APIconvenios.Enums;

namespace APIconvenios.DTOs.Involucrados
{
    public class InvolucradosDto
    {
        public int Id { get; set; }
        public string NombreCompleto => $"{Nombre} {Apellido}";
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public int Legajo { get; set; }

        public string RolInvolucrado { get; set; }
    }
}
