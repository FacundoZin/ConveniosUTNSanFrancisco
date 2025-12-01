using APIconvenios.Common.Enums;
using System.ComponentModel;

namespace APIconvenios.DTOs.ConvenioMarco
{
    public class InsertConvenioMarcoDto
    {
        public string? numeroconvenio { get; set; }
        public string Titulo { get; set; }
        public DateOnly? FechaFirmaConvenio { get; set; }
        public DateOnly? FechaFin { get; set; }
        public string? ComentarioOpcional { get; set; }

        [DefaultValue(EstadoConvenio.Borrador)]
        public EstadoConvenio Estado { get; set; } = EstadoConvenio.Borrador;
        public string? NumeroResolucion { get; set; }

        [DefaultValue(false)]
        public bool Refrendado { get; set; } = false;
    }
}
