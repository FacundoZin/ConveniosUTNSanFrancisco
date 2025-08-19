using APIconvenios.Common;
using System.ComponentModel.DataAnnotations;

namespace APIconvenios.Helpers.Validators
{
    public class ConvenioQueryObjectValidator
    {
        public List<string> Errors { get; private set; } = new List<string>();

        public List<string> Validate(ConvenioQueryObject queryObject)
        {
            var errors = new List<string>();

            if (queryObject == null)
                errors.Add("El queryObject es requerido");

            if (queryObject != null)  // Para evitar null reference
            {
                if (queryObject.AntiguedadDescendente && queryObject.AntiguedadAscendente)
                    errors.Add("No podés ordenar por antigüedad ascendente y descendente al mismo tiempo");

                if (queryObject.ProximosAterminar &&
                   (queryObject.AntiguedadDescendente || queryObject.AntiguedadAscendente))
                    errors.Add("Solo podes elegir una opción de ordenamiento");

                if (queryObject.PaginaActual < 1)
                    errors.Add("La pagina actual no puede ser menor a 1");

                if (queryObject.CantidadResultados < 5 || queryObject.CantidadResultados > 20)
                    errors.Add("La cantidad de resultados debe estar entre 5 y 20");
            }

            return errors;
        }
    }
}
