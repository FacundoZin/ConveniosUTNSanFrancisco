using APIconvenios.DTOs.Empresa;
using APIconvenios.Models;

namespace APIconvenios.DTOs.ConvenioMarco
{
    public class CargarConvenioMarcoRequestDto
    {
        public InsertConvenioMarcoDto InsertConvenioDto { get; set; }
        public InsertEmpresaDto? InsertEmpresaDto { get; set; }   
        public string? NumeroConvEspecificoParaVincular { get; set; }
    }
}
