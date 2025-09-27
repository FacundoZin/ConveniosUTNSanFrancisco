using APIconvenios.DTOs.Empresa;
using APIconvenios.Models;

namespace APIconvenios.DTOs.ConvenioMarco
{
    public class UpdateConvenioMarcoRequetsDto
    {
        public UpdateConvenioMarcoDto UpdateConvenioMarcoDto { get; set; }
        public InsertEmpresaDto? InsertEmpresaDto { get; set; }
        public int[]? IdsConveniosEspecificosParaVincular { get; set; }
        public int[]? IdsConveniosEspecificosParaDesvincular { get; set; }
        public bool EmpresaDesvinculada { get; set; } = false;
    }
}
