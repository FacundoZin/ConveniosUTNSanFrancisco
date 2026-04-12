using APIconvenios.DTOs.ConvenioEspecifico;

namespace APIconvenios.DTOs.Involucrado
{
    public class InvolucradosWithConveniosDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public List<ConvenioEspecificoDto> conveniosEspecificos { get; set; }
    }
}
