using APIconvenios.Common;

namespace APIconvenios.DTOs.Filters
{
    public class ByAntiguedadDto
    {
        public bool asendente { get; set; } = true;
        public ConvenioType ConvenioType { get; set; }
    }
}
