using APIconvenios.Enums;

namespace APIconvenios.Models
{
    public class Involucrados
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public int Legajo { get; set; }
        public Roles RolInvolucrado { get; set; }  
        
        public List<ConvenioEspecifico> ConveniosEspecificos { get; set; }  
    }
}
