using APIconvenios.Common;

namespace APIconvenios.DTOs.Filters
{
    public class ByFechaFinDto
    {
        public DateOnly FechaFin { get; set; }
        public ConvenioType convenioType { get; set; }
    }
}
