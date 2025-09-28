using APIconvenios.Common;

namespace APIconvenios.DTOs.Filters
{
    public class ByNumeroConvenioDto
    {
        public string NumeroConvenio { get; set; } = string.Empty;  
        public ConvenioType ConvenioType { get; set; }
    }
}
