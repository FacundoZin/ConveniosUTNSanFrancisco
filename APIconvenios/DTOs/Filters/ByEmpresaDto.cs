using APIconvenios.Common;

namespace APIconvenios.DTOs.Filters
{
    public class ByEmpresaDto
    {
        public string EmpresaName { get; set; } = string.Empty;
        public ConvenioType convenioType { get; set; }
    }
}
