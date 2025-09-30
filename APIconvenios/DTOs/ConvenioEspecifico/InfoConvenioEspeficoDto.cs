using APIconvenios.Common.Enums;
using APIconvenios.DTOs.Archivo;
using APIconvenios.DTOs.Empresa;
using APIconvenios.DTOs.Involucrados;
using APIconvenios.Helpers.Validators;
using APIconvenios.Models;
using System.ComponentModel.DataAnnotations;

namespace APIconvenios.DTOs.ConvenioEspecifico
{
    public class InfoConvenioEspeficoDto
    {
        public int Id { get; set; }
        public string? numeroconvenio { get; set; }
        public string? Titulo { get; set; }
        public DateOnly? FechaFirmaConvenio { get; set; }
        public DateOnly? FechaInicioActividades { get; set; }
        public DateOnly? FechaFinConvenio { get; set; }
        public string? ComentarioOpcional { get; set; }
        public EstadoConvenio? Estado { get; set; }
        public bool EsActa { get; set; } = false;
        public string? NumeroResolucion { get; set; }
        public bool Refrendado { get; set; } = false;


        public int? ConvenioMarcoId { get; set; }
        public EmpresaDto? empresa { get; set; }
        public List<InvolucradosDto>? Involucrados { get; set; }
        public List<viewArchivoDto>? DocumentosAdjuntos { get; set; }
        public List<Carreras>? CarrerasInvolucradas { get; set; }
    }
}
