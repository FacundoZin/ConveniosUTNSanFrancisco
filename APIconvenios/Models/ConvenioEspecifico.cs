using APIconvenios.Enums;

namespace APIconvenios.Models
{
    public class ConvenioEspecifico
    {
        public int Id { get; set; }
        public int numeroconvenio { get; set; }
        public string Titulo { get; set; }
        public DateOnly FechaFirmaConvenio { get; set; }
        public DateOnly FechaInicioActividades { get; set; }
        public DateOnly FechaFinConvenio { get; set; }  

        public int ConvenioMarcoId { get; set; }
        public ConvenioMarco ConvenioMarco { get; set; }
        
        public List <Involucrados> Involucrados { get; set; }
        public Secretarias SecretariaInvolucrada { get; set; }

    }
}
