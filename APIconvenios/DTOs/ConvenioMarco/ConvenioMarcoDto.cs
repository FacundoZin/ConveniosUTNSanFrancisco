using APIconvenios.Common.Enums;
using APIconvenios.Models;
using System.ComponentModel.DataAnnotations;

namespace APIconvenios.DTOs.ConvenioMarco
{
    public class ConvenioMarcoDto
    {
        public int Id { get; set; }
        public string? Titulo { get; set; } = string.Empty;  
        public string? numeroconvenio { get; set; }
        public string? NombreEmpresa { get; set; }  
        public DateOnly? FechaFirmaConvenio { get; set; }
        public DateOnly? FechaFin { get; set; }
        public string ConvenioType { get; set; } = "marco";
        public EstadoConvenio Estado { get; set; }
        public bool Refrendado { get; set; }
    }
}
