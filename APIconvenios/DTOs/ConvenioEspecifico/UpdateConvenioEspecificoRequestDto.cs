using APIconvenios.DTOs.Empresa;
using APIconvenios.DTOs.Involucrados;
using APIconvenios.Models;

namespace APIconvenios.DTOs.ConvenioEspecifico
{
    public class UpdateConvenioEspecificoRequestDto
    {
        public UpdateConvenioEspecificoDto UpdateConvenioDto { get; set; }
        public List<InsertInvolucradosDto>? InsertInvolucradosDtos { get; set; } 
        public int[]? idCarreras { get; set; }
        public InsertEmpresaDto? empresaDto { get; set; }
        public int[]? IdsInvolucraodsEliminados { get; set; }
        public int? IdConvenioMarcoVinculado { get; set; }
        public bool DesvincularConvenioMarco { get; set; } = false;
        public bool DesvincularEmpresa { get; set; } = false;
    }
}
