namespace APIconvenios.DTOs.Empresa
{
    public class CreateEmpresaDto
    {
        public string Nombre { get; set; }
        public string? RazonSocial { get; set; }
        public int? Cuit { get; set; }
        public string? Direccion { get; set; }
        public int? Telefono { get; set; }
        public string? Email { get; set; }
    }
}
