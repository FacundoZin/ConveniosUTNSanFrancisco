using APIconvenios.DTOs.Filters;
using System.ComponentModel.DataAnnotations;

namespace APIconvenios.Common
{
    public class ConvenioQueryObject
    {
        //Barra de busqueda
        public ByTitleDto? Titulo { get; set; }
        public ByNumeroResolucionDto? NumeroResolucion { get; set; }
        public ByNumeroConvenioDto? NumeroConvenio { get; set; }
        public ByEmpresaDto? Empresa { get; set; }

        //buscar actas
        public ByIsActaDto? IsActa { get; set; }

        //buscar convenios refrendados
        public ByRefrendadoDto? Refrendado { get; set; }

        //filtrar por estado
        public ByEstadoConvenioDto? Estado { get; set; }

        //filtrar por carreras involucradas
        public ByCarreraInvolucradaDto? Carrera { get; set; }

        //buscar por fechas
        public ByFechaFirmaDto? FechaFirma { get; set; }
        public ByFechaFinDto? FechaFin { get; set; }

        //ordenar por antiguedad
        public ByAntiguedadDto? AntiguedadDto { get; set; }

        //buscar convenios proximos a vencimiento
        public ByProximosAvencerDto? ProximosAvencer { get; set; }


        public int PaginaActual { get; set; } = 1;
        public int CantidadResultados { get; set; } = 10;

    }
}
