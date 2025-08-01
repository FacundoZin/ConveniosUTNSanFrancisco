using APIconvenios.DTOs.ConvenioEspecifico;
using APIconvenios.DTOs.Empresa;
using APIconvenios.Models;
using APIconvenios.Validations;
using System.ComponentModel.DataAnnotations;

namespace APIconvenios.DTOs.ConvenioMarco
{
    public class InfoConvenioMarcoDto
    {
        public int Idconvenio { get; set; }
        public int numeroconvenio { get; set; }
        public DateOnly FechaFirmaConvenio { get; set; }
        public DateOnly FechaFin { get; set; }
        public string ComentarioOpcional { get; set; }
        public EmpresaDto empresa { get; set; }

        public List<ConvenioEspecificoDto> ConveniosEspecificos { get; set; }

    }
}
