using APIconvenios.DTOs.ConvenioEspecifico;
using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.Models;

namespace APIconvenios.DTOs.Empresa
{
    public class EmpresaWithConveniosDto
    {
        public string NombreEmpresa { get; set; }   
        public ConvenioMarcoDto? ConvenioMarco { get; set; }
        public List<ConvenioEspecificoDto> conveniosEspecificos { get; set; }  
    }
}
