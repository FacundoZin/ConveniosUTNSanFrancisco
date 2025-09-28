using APIconvenios.DTOs.Filters;
using System.ComponentModel.DataAnnotations;

namespace APIconvenios.Common
{
    public class ConvenioQueryObject
    {
        public ByTitleDto? Titulo { get; set; }
        public ByRefrendadoDto? Refrendado { get; set; }
        public ByNumeroResolucionDto? NumeroResolucion { get; set; }
        public ByNumeroConvenioDto? NumeroConvenio { get; set; }
        public ByIsActaDto? IsActa { get; set; }
        public ByFechaFirmaDto? FechaFirma { get; set; }
        public ByFechaFinDto? FechaInicio { get; set; }
        public ByEstadoConvenioDto? Estado { get; set; }
        public ByEmpresaDto? Empresa { get; set; }
        public ByCarreraInvolucradaDto? Carrera { get; set; }
        public ByAntiguedadDto? AntiguedadDto { get; set; }
        public ByProximosAvencerDto? ProximosAvencer { get; set; }


        public int PaginaActual { get; set; } = 1;
        public int CantidadResultados { get; set; } = 10;

    }
}
