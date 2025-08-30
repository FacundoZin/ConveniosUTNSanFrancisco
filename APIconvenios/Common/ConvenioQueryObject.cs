using System.ComponentModel.DataAnnotations;

namespace APIconvenios.Common
{
    public class ConvenioQueryObject
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string TituloConvenio { get; set; } = string.Empty;

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Nombre_empresa { get; set; } = string.Empty;
        public bool ProximosAterminar { get; set; } = false;
        public bool AntiguedadDescendente { get; set; } = false;
        public bool AntiguedadAscendente { get; set; } = false;

        public int PaginaActual { get; set; } = 1;
        public int CantidadResultados { get; set; } = 10;

    }
}
