using APIconvenios.Common;
using APIconvenios.Models;
using System.Linq.Expressions;

namespace APIconvenios.Helpers.Query
{
    public static class _QueryBuilder
    {
        public static (Expression<Func<ConvenioMarco, bool>> filtro,
                   Func<IQueryable<ConvenioMarco>, IOrderedQueryable<ConvenioMarco>>? ordenamiento)
        GenerarParaConvenioMarco(ConvenioQueryObject queryObject)
        {
            Expression<Func<ConvenioMarco, bool>> filtro = c =>
            (string.IsNullOrEmpty(queryObject.Nombre_empresa) || c.Empresa.Nombre.Contains(queryObject.Nombre_empresa)) &&
            (string.IsNullOrEmpty(queryObject.TituloConvenio) || c.Titulo.Contains(queryObject.TituloConvenio));

            Func<IQueryable<ConvenioMarco>, IOrderedQueryable<ConvenioMarco>>? ordenamiento = null;

            if (queryObject.AntiguedadDescendente)
                ordenamiento = q => q.OrderByDescending(c => c.FechaFirmaConvenio);

            if (queryObject.AntiguedadAscendente)
                ordenamiento = q => q.OrderBy(c => c.FechaFirmaConvenio);

            if (queryObject.ProximosAterminar)
                ordenamiento = q => q.OrderBy(c => c.FechaFin);

            return (filtro, ordenamiento);  
        }

        public static (Expression<Func<ConvenioEspecifico, bool>> filtro,
           Func<IQueryable<ConvenioEspecifico>, IOrderedQueryable<ConvenioEspecifico>>? ordenamiento)
            GenerarParaConvenioEspecifico(ConvenioQueryObject queryObject)
        {
            Expression<Func<ConvenioEspecifico, bool>> filtro = c =>
            (string.IsNullOrEmpty(queryObject.Nombre_empresa) || c.ConvenioMarco.Empresa.Nombre.Contains(queryObject.Nombre_empresa)) &&
            (string.IsNullOrEmpty(queryObject.TituloConvenio) || c.Titulo.Contains(queryObject.TituloConvenio));

            Func<IQueryable<ConvenioEspecifico>, IOrderedQueryable<ConvenioEspecifico>>? ordenamiento = null;

            if (queryObject.AntiguedadDescendente)
                ordenamiento = q => q.OrderByDescending(c => c.FechaFirmaConvenio);

            if (queryObject.AntiguedadAscendente)
                ordenamiento = q => q.OrderBy(c => c.FechaFirmaConvenio);

            if (queryObject.ProximosAterminar)
                ordenamiento = q => q.OrderBy(c => c.FechaFinConvenio);

            return (filtro, ordenamiento);
        }
    }
}
