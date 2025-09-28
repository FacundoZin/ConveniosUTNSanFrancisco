using APIconvenios.Common;

namespace APIconvenios.DTOs.Filters
{
    public class ByNumeroResolucionDto
    {
        public string NumeroResolucion { get; set; } = string.Empty;
        public ConvenioType ConvenioType { get; set; }
    }
}
