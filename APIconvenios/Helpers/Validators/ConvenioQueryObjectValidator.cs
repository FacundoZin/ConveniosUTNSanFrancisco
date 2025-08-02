using APIconvenios.Filters;
using System.ComponentModel.DataAnnotations;

namespace APIconvenios.Helpers.Validators
{
    public class ConvenioQueryObjectValidator
    {
        public List<string> Errors { get; private set; } = new List<string>();

        public void Validate(ConvenioQueryObject dto)
        {

            if (dto == null)
                Errors.Add("El queryObject es requerido");


            if (dto.AntiguedadDescendente && dto.AntiguedadAscendente)
                Errors.Add("No podés ordenar por antigüedad ascendente y descendente al mismo tiempo");

            if (dto.ProximosAterminar &&
               (dto.AntiguedadDescendente || dto.AntiguedadAscendente))
                Errors.Add("Solo podes elejir una opcion de ordenamiento");
        }
    }
}
