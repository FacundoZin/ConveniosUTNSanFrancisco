using APIconvenios.Filters;
using System.ComponentModel.DataAnnotations;

namespace APIconvenios.Helpers.Validators
{
    public class ConvenioQueryObjectValidator
    {
        public List<string> Errors { get; private set; } = new List<string>();

        public void Validate(ConvenioQueryObject queryObject)
        {

            if (queryObject == null)
                Errors.Add("El queryObject es requerido");


            if (queryObject.AntiguedadDescendente && queryObject.AntiguedadAscendente)
                Errors.Add("No podés ordenar por antigüedad ascendente y descendente al mismo tiempo");

            if (queryObject.ProximosAterminar &&
               (queryObject.AntiguedadDescendente || queryObject.AntiguedadAscendente))
                Errors.Add("Solo podes elejir una opcion de ordenamiento");

            if (queryObject.PaginaActual < 1) Errors.Add("la pagina actual no puede ser menor a 1");

            if (queryObject.CantidadResultados < 5 || queryObject.CantidadResultados > 20) Errors.Add("La cantidad de resultados debe estar entre 5 y 20");
        }
    }
}
