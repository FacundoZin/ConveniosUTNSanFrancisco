using APIconvenios.DTOs.Empresa;

namespace APIconvenios.DTOs.ConvenioMarco
{
    public class CargarConvenioMarcoRequestDto
    {
        public CreateConvenioMarcoDto ConvenioDto { get; set; }
        public InsertEmpresaDto EmpresaDto { get; set; }   
    }
}
