using APIconvenios.Common.Enums;

namespace APIconvenios.DTOs.Filters
{
    public class ByEstadoConvenioDto
    {
        public EstadoConvenio Estado { get; set; }
        public string convenioType { get; set; } = string.Empty;
    }
}
