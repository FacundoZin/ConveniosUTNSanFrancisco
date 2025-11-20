using APIconvenios.DTOs.Empresa;
using APIconvenios.DTOs.Involucrados;
using APIconvenios.Models;
using System.ComponentModel;

namespace APIconvenios.DTOs.ConvenioEspecifico
{
    public class UpdateConvenioEspecificoRequestDto
    {
        public UpdateConvenioEspecificoDto UpdateConvenioDto { get; set; }
        public List<InsertInvolucradosDto>? InsertInvolucradosDtos { get; set; } 
        public int[]? idCarreras { get; set; }
        public InsertEmpresaDto? InsertEmpresaDto { get; set; }
        public int[]? IdsInvolucraodsEliminados { get; set; }
        public string? numeroConvenioMarcoVinculado { get; set; }

        [DefaultValue(false)]
        public bool DesvincularConvenioMarco { get; set; } = false;

        [DefaultValue(false)]
        public bool DesvincularEmpresa { get; set; } = false;
    }
}
