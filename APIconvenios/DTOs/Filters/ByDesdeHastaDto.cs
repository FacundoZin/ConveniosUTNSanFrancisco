namespace APIconvenios.DTOs.Filters
{
    public class ByDesdeHastaDto
    {
        public DateOnly desde { get; set; }
        public DateOnly hasta { get; set; }
        public string convenioType { get; set; } = string.Empty;
    }
}
