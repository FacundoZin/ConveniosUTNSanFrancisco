using APIconvenios.Common.Enums;
using APIconvenios.Helpers.Validators;
using System.ComponentModel.DataAnnotations;

namespace APIconvenios.DTOs.ConvenioMarco
{
    public class UpdateConvenioMarcoDto
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
    }
}
