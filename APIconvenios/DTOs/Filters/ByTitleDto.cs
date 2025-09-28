using APIconvenios.Common;

namespace APIconvenios.DTOs.Filters
{
    public class ByTitleDto
    {
        public string Title { get; set; } = string.Empty;
        public ConvenioType ConvenioType { get; set; }
    }
}
