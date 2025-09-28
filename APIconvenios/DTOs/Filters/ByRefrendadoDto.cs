using APIconvenios.Common;

namespace APIconvenios.DTOs.Filters
{
    public class ByRefrendadoDto
    {
        public bool refrendado { get; set; } = false;
        public ConvenioType convenioType { get; set; }
    }
}
