using APIconvenios.Models;

namespace APIconvenios.DTOs.Empresa
{
    public class EmpresaWithConveniosDto
    {
        public string NombreEmpresa { get; set; }   
        public APIconvenios.Models.ConvenioMarco? ConvenioMarco { get; set; }
        public List<APIconvenios.Models.ConvenioEspecifico> conveniosEspecificos { get; set; } 
    }
}
