using APIconvenios.Common.Enums;

namespace APIconvenios.DTOs.ConvenioMarco
{
    public class InsertConvenioMarcoDto
    {
        public int Id { get; set; }
        public string? numeroconvenio { get; set; }
        public string? Titulo { get; set; }
        public DateOnly? FechaFirmaConvenio { get; set; }
        public DateOnly? FechaFin { get; set; }
        public string? ComentarioOpcional { get; set; }
        public EstadoConvenio Estado { get; set; } = EstadoConvenio.Borrador;
        public string? NumeroResolucion { get; set; }
        public bool Refrendado { get; set; } = false;
    }
}
