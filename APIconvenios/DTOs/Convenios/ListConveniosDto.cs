
using APIconvenios.DTOs.ConvenioEspecifico;
using APIconvenios.DTOs.ConvenioMarco;

namespace APIconvenios.DTOs.Convenios
{
    public class ListConveniosDto
    {
        public List<ConvenioMarcoDto> conveniosMarcos { get; set; }
        public List<ConvenioEspecificoDto> convenioEspecificos { get; set; }
    }
}
