using APIconvenios.DTOs.Involucrados;

namespace APIconvenios.DTOs.ConvenioEspecifico
{
    public class CargarConvenioEspecificoRequestDto
    {
        public InsertConvenioEspecificoDto ConvenioDto { get; set; }
        public List<InsertInvolucradosDto> InvolucradosDto { get; set; }
    }
}
