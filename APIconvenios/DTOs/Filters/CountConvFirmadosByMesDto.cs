namespace APIconvenios.DTOs.Filters
{
    public class CountConvFirmadosByMesDto
    {
        public int month { get; set; }
        public int year { get; set; }
        public string convenioType { get; set; } = string.Empty;
    }
}
