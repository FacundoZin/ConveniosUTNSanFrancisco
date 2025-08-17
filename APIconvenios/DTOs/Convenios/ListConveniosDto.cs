using APIconvenios.DTOs.ConvenioEspecifico;
using APIconvenios.DTOs.ConvenioMarco;

namespace APIconvenios.DTOs.Convenios
{
    public class ListConveniosDto
    {
        public List<ConvenioEspecificoDto> ConveniosEspecificos { get; set; }
        public List<ConvenioMarcoDto> ConveniosMarco { get; set; }
    }
}
