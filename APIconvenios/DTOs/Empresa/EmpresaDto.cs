namespace APIconvenios.DTOs.Empresa
{
    public class EmpresaDto
    {
        public int Id { get; set; } 
        public string Nombre_Empresa { get; set; }
        public string RazonSocial { get; set; }
        public long Cuit { get; set; }
        public string Direccion_Empresa { get; set; }
        public string Telefono_Empresa { get; set; }
        public string Email_Empresa { get; set; }
    }
}
