using APIconvenios.Common.Enums;
using APIconvenios.Helpers.Validators;
using System.ComponentModel.DataAnnotations;

namespace APIconvenios.DTOs.ConvenioEspecifico
{
    public class ConvenioEspecificoDto
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }

        public string? numeroconvenio { get; set; }
        public string? NombreEmpresa { get; set; }

        public DateOnly? FechaFirmaConvenio { get; set; }
        public DateOnly? FechaInicioActividades { get; set; }
        public DateOnly? FechaFin { get; set; }
        public string ConvenioType { get; set; } = "especifico";
        public EstadoConvenio? Estado { get; set; }
        public bool EsActa { get; set; } 
        public bool Refrendado { get; set; }
    }
}
