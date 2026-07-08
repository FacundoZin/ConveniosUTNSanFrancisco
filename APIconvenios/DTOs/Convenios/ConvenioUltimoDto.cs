using APIconvenios.Common.Enums;

namespace APIconvenios.DTOs.Convenios
{
    public class ConvenioUltimoDto
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string ConvenioType { get; set; } = string.Empty;
        public string? NombreEmpresa { get; set; }
        public EstadoConvenio Estado { get; set; }
    }

    public class UltimosConveniosDto
    {
        public List<ConvenioUltimoDto> ConveniosMarcos { get; set; } = new();
        public List<ConvenioUltimoDto> ConveniosEspecificos { get; set; } = new();
    }
}
