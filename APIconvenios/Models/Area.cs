namespace APIconvenios.Models
{
    public class Area
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public List<ConvenioEspecifico> ConveniosInvolucrados { get; set; }    
        public List<Involucrados> Involucrados { get; set; }
    }
}
