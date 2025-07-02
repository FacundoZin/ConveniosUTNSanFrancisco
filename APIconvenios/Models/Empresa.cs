namespace APIconvenios.Models
{
    public class Empresa
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? RazonSocial { get; set; }
        public int? Cuit { get; set; }
        public string? Direccion { get; set; }
        public int? Telefono { get; set; }
        public string? Email { get; set; }
    }
}
