using APIconvenios.Common.Enums;

namespace APIconvenios.Models
{
    public class ConvenioEspecifico
    {
        public int Id { get; set; }
        public string? numeroconvenio { get; set; }
        public string? TituloConvenio { get; set; }
        public DateOnly? FechaFirmaConvenio { get; set; }
        public DateOnly? FechaInicioActividades { get; set; }
        public DateOnly? FechaFinConvenio { get; set; }
        public string? ComentarioOpcional { get; set; }
        public EstadoConvenio? Estado { get; set; }
        public bool EsActa { get; set; } = false;
        public string? NumeroResolucion { get; set; }
        public bool Refrendado { get; set; } = false;


        public int? ConvenioMarcoId { get; set; }
        public ConvenioMarco? ConvenioMarco { get; set; }

        public int? EmpresaId { get; set; }
        public Empresa? empresa { get; set; }

        public List<ArchivosAdjuntos>? ArchivosAdjuntos { get; set; } = new List<ArchivosAdjuntos>();

        public List<Involucrados>? Involucrados { get; set; } = new List<Involucrados>();

        public List<Carreras>? CarrerasInvolucradas { get; set; } = new List<Carreras>();

    }
}
