using APIconvenios.DTOs.Filters;
using System.ComponentModel.DataAnnotations;

namespace APIconvenios.Common
{
    public class ConvenioQueryObject
    {
        //Barra de busqueda
        public ByTitleDto? ByTitulo { get; set; }
        public ByNumeroResolucionDto? ByNumeroResolucion { get; set; }
        public ByNumeroConvenioDto? ByNumeroConvenio { get; set; }
        public ByEmpresaDto? ByEmpresa { get; set; }

        //buscar actas
        public ByIsActaDto? ByIsActa { get; set; }

        //buscar convenios refrendados
        public ByRefrendadoDto? ByIsRefrendado { get; set; }

        //filtrar por estado
        public ByEstadoConvenioDto? ByEstado { get; set; }

        //filtrar por carreras involucradas
        public ByCarreraInvolucradaDto? ByCarrera { get; set; }

        //buscar por fechas
        public ByFechaFirmaDto? ByFechaFirma { get; set; }
        public ByFechaFinDto? ByFechaFin { get; set; }

        //ordenar por antiguedad
        public ByAntiguedadDto? ByAntiguedadDto { get; set; }

        //buscar convenios proximos a vencimiento
        public ByProximosAvencerDto? ByProximosAvencer { get; set; }


        public int PaginaActual { get; set; } = 1;
        public int CantidadResultados { get; set; } = 10;

    }
}
