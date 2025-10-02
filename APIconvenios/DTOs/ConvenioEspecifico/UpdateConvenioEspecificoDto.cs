using APIconvenios.Common.Enums;
using APIconvenios.Helpers.Validators;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APIconvenios.DTOs.ConvenioEspecifico
{
    public class UpdateConvenioEspecificoDto
    {
        public int Id { get; set; }
        public string? numeroconvenio { get; set; }
        public string? Titulo { get; set; }
        public DateOnly? FechaFirmaConvenio { get; set; }
        public DateOnly? FechaInicioActividades { get; set; }
        public DateOnly? FechaFinConvenio { get; set; }
        public string? ComentarioOpcional { get; set; }

        [DefaultValue(EstadoConvenio.Borrador)]
        public EstadoConvenio? Estado { get; set; }

        [DefaultValue(false)]
        public bool EsActa { get; set; }
        public string? NumeroResolucion { get; set; }

        [DefaultValue(false)]
        public bool Refrendado { get; set; }

        public int? ConvenioMarcoId { get; set; }
    }
}
