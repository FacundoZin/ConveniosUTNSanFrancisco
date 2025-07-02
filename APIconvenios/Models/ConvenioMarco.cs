namespace APIconvenios.Models
{
    public class ConvenioMarco
    {
        public int Id { get; set; }
        public int numeroconvenio { get; set; }

        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        public DateOnly FechaFirmaConvenio { get; set; }
        public DateOnly FechaFin { get; set; }

        public List<ConvenioEspecifico> ConveniosEspecificos { get; set; }
    }
}
