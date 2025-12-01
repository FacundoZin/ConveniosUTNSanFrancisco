using System.ComponentModel.DataAnnotations;

namespace APIconvenios.Models
{
    public class Empresa
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? RazonSocial { get; set; }
        public string? Cuit { get; set; } 
        public string? Direccion { get; set; }
        public string? Telefono { get; set; } 

        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string? Email { get; set; }

        public int? ConvenioMarcoId { get; set; }    
        public ConvenioMarco? ConvenioMarco { get; set; } 
        public List<ConvenioEspecifico>? ConveniosEspecificos { get; set; } = new List<ConvenioEspecifico>();
    }
}
