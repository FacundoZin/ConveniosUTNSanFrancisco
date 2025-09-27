namespace APIconvenios.Models
{
    public class Carreras
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public List<ConvenioEspecifico> ConveniosInvolucrados { get; set; }    
    }
}
