using APIconvenios.Common.Enums;

namespace APIconvenios.Models
{
    public class ConvenioMarco
    {
        public int Id { get; set; }
        public string? numeroconvenio { get; set; }
        public string? Titulo { get; set; }  
        public DateOnly? FechaFirmaConvenio { get; set; }
        public DateOnly? FechaFin { get; set; }
        public string? ComentarioOpcional { get; set; }
        public EstadoConvenio Estado { get; set; }
        public string? NumeroResolucion { get; set; }
        public bool Refrendado { get; set; } = false;

        public List<ConvenioEspecifico>? ConveniosEspecificos { get; set; }
        public int? EmpresaId { get; set; }
        public Empresa? Empresa { get; set; }
        public List<ArchivosAdjuntos>? ArchivosAdjuntos { get; set; }
    }
}
