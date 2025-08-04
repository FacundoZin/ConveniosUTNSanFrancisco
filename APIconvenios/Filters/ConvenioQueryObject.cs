namespace APIconvenios.Filters
{
    public class ConvenioQueryObject
    {
        public string titulo { get; set; } = string.Empty;
        public string empresa { get; set; } = string.Empty;
        public bool ProximosAterminar { get; set; } = false;
        public bool AntiguedadDescendente { get; set; } = false;
        public bool AntiguedadAscendente { get; set; } = false;

        public int PaginaActual { get; set; } = 1;
        public int CantidadResultados { get; set; } = 10;

    }
}
