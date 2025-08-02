using APIconvenios.Validations;
using System.ComponentModel.DataAnnotations;

namespace APIconvenios.DTOs.ConvenioMarco
{
    public class UpdateConvenioMarcoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El número de convenio es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El número de convenio debe ser mayor a 0.")]
        public int numeroconvenio { get; set; }

        [Required(ErrorMessage = "El convenio debe tener un titulo que lo identifique")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "La fecha de firma del convenio es obligatoria.")]
        public DateOnly FechaFirmaConvenio { get; set; }

        [Required(ErrorMessage = "La fecha de finalización es obligatoria.")]
        [ValidacionFechas("FechaFirmaConvenio", ErrorMessage = "La fecha de finalización debe ser posterior a la fecha de firma.")]
        public DateOnly FechaFin { get; set; }

        public string ComentarioOpcional { get; set; }
    }
}
