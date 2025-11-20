using APIconvenios.DTOs.Empresa;
using APIconvenios.Models;
using System.ComponentModel;

namespace APIconvenios.DTOs.ConvenioMarco
{
    public class UpdateConvenioMarcoRequetsDto
    {
        public UpdateConvenioMarcoDto UpdateConvenioMarcoDto { get; set; }
        public InsertEmpresaDto? InsertEmpresaDto { get; set; }
        public string? NumeroConvenioEspecificosParaVincular { get; set; }
        public int[]? IdsConveniosEspecificosParaDesvincular { get; set; }

        [DefaultValue(false)]
        public bool EmpresaDesvinculada { get; set; } = false;
    }
}
