using APIconvenios.Enums;
using APIconvenios.Helpers.Validators;
using APIconvenios.Models;
using System.ComponentModel.DataAnnotations;

namespace APIconvenios.DTOs.ConvenioEspecifico
{
    public class CreateConvenioEspecificoDto
    {
        [Required(ErrorMessage = "El número de convenio es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El número de convenio debe ser mayor a 0.")]
        public int numeroconvenio { get; set; }

        [Required(ErrorMessage = "El convenio debe tener un titulo que lo identifique.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "La fecha de firma del convenio es obligatoria.")]
        public DateOnly FechaFirmaConvenio { get; set; }

        [Required(ErrorMessage = "La fecha de inicio de actividades es obligatoria")]
        [ValidacionFechas("FechaFirmaConvenio", ErrorMessage = "La fecha de inicio de actividades debe ser posterior a la fecha de firma.")]
        public DateOnly FechaInicioActividades { get; set; }

        [Required(ErrorMessage = "La fecha de finalización del convenio es obligatoria.")]
        [ValidacionFechas("FechaInicioActividades", ErrorMessage = "La fecha de finalizacion de actividades debe ser posterior a la fecha de inicio de las mismas")]
        public DateOnly FechaFinConvenio { get; set; }

        [Required(ErrorMessage = "El convenio especifico debe estar asociado a un convenio marco")]
        public int ConvenioMarcoId { get; set; }
        public string ComentarioOpcional { get; set; }
    }
}
