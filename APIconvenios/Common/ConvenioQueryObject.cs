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
        public ByIsActaDto? ByIsActa { get; set; }
        public ByRefrendadoDto? ByIsRefrendado { get; set; }
        public ByEstadoConvenioDto? ByEstado { get; set; }
        public ByCarreraInvolucradaDto? ByCarrera { get; set; }
        public ByFechaFirmaDto? ByFechaFirma { get; set; }
        public ByFechaFinDto? ByFechaFin { get; set; }
        public ByAntiguedadDto? ByAntiguedadDto { get; set; }
        public ByProximosAvencerDto? ByProximosAvencer { get; set; }
        public ByMesDto? ByMes { get; set; }
        public ByAñoDto? ByAño { get; set; }
        public ByDesdeHastaDto? ByDesdeHastaDto { get; set; }


        public int PaginaActual { get; set; } = 1;
        public int CantidadResultados { get; set; } = 10;

    }
}
