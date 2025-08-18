namespace APIconvenios.DTOs.Convenios
{
    public class UploadConvenioDocumentDto
    {
        public IFormFile File { get; set; }
        public int IdConvenio { get; set; }
        public string ConvenioType { get; set; }
    }
}
