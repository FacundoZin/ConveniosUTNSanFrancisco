using System.ComponentModel.DataAnnotations;

namespace APIconvenios.DTOs.Archivo
{
    public class InsertArchivoDto
    {
        [Required]
        public string NombreArchivo { get; set; }

        [Required]
        public string RutaArchivo { get; set; }

        public IFormFile file { get; set; } 

        public int? ConvenioEspecificoId { get; set; }
        public int? ConvenioMarcoId { get; set; }
    }
}
