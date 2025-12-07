using APIconvenios.DTOs.Empresa;
using APIconvenios.DTOs.Involucrados;
using APIconvenios.Models;

namespace APIconvenios.DTOs.ConvenioEspecifico
{
    public class CargarConvenioEspecificoRequestDto
    {
        public InsertConvenioEspecificoDto InsertConvenioDto { get; set; }
        public List<InsertInvolucradosDto>? InsertInvolucradosDto { get; set; }
        public int[]? idCarreras { get; set; }
        public InsertEmpresaDto? InsertEmpresaDto { get; set; }
        public int? idConvenioMarco { get; set; }
    }
}
