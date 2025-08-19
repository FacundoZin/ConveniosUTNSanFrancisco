using APIconvenios.Helpers.Validators;
using System.ComponentModel.DataAnnotations;

namespace APIconvenios.DTOs.ConvenioEspecifico
{
    public class ConvenioEspecificoDto
    {
        public int Id { get; set; }
        public int numeroconvenio { get; set; }
        public string Titulo { get; set; }
        public DateOnly FechaFirmaConvenio { get; set; }
        public DateOnly FechaInicioActividades { get; set; }
        public DateOnly FechaFinConvenio { get; set; }
        public string ConvenioType { get; set; } = "especifico";
    }
}
