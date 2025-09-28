using APIconvenios.Common;

namespace APIconvenios.DTOs.Filters
{
    public class ByFechaFirmaDto
    {
        public DateOnly? FechaInicio { get; set; }  
        public ConvenioType convenioType { get; set; }
    }
}
