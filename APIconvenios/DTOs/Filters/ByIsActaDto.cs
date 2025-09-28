using APIconvenios.Common;

namespace APIconvenios.DTOs.Filters
{
    public class ByIsActaDto
    {
        public bool IsActa { get; set; } = false;
        public ConvenioType ConvenioType { get; set; }
    }
}
