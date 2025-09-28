using APIconvenios.Common;
using APIconvenios.Common.Enums;

namespace APIconvenios.DTOs.Filters
{
    public class ByEstadoConvenioDto
    {
        public EstadoConvenio Estado { get; set; }
        public ConvenioType convenioType { get; set; }  
    }
}
