namespace APIconvenios.Models
{
    public class ArchivosAdjuntos
    {
        public int Id { get; set; }
        public string NombreArchivo { get; set; }
        public string RutaArchivo { get; set; }
        public string ContentType { get; set; }  
        public int? ConvenioEspecificoId { get; set; }
        public int? ConvenioMarcoId { get; set; }
    }
}
