namespace APIconvenios.DTOs.Filters
{
    public class ByFechaFinDto
    {
        public DateOnly FechaFin { get; set; }
        public string convenioType { get; set; } = string.Empty;
    }
}
