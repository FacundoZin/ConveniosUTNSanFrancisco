using System.ComponentModel.DataAnnotations;

namespace APIconvenios.DTOs.Empresa
{
    public class InsertEmpresaDto
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string? RazonSocial { get; set; }
        public string? Cuit { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
    }
}
