namespace APIconvenios.DTOs.Filters
{
    public class ByFechaFirmaDto
    {
        public DateOnly? FechaInicio { get; set; }
        public string convenioType { get; set; } = string.Empty;
    }
}
