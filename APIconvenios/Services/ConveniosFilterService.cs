using APIconvenios.Commands.FilterCommands.Commands;
using APIconvenios.Common;
using APIconvenios.DTOs.Convenios;
using APIconvenios.Interfaces.Servicios;
using APIconvenios.UnitOfWork;

namespace APIconvenios.Services
{
    public class ConveniosFilterService : IConvenioFilterService
    {

        private readonly _UnitOfWork _UnitOfWork;

        public ConveniosFilterService(_UnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        public async Task<Result<object>> ListarConvenios(ConvenioQueryObject queryObject)
        {
            if (queryObject.ByTitulo != null)
            {
                var cmd = new SearchByTitleCmd(queryObject);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return ApplyPagination(result, queryObject);
            }
            else if (queryObject.ByNumeroResolucion != null)
            {
                var cmd = new SearchByNumeroResolucionCmd(queryObject);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return ApplyPagination(result, queryObject);
            }
            else if (queryObject.ByNumeroConvenio != null)
            {
                var cmd = new SearchByNumeroConvenioCmd(queryObject);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return ApplyPagination(result, queryObject);
            }
            else if (queryObject.ByEmpresa != null)
            {
                var cmd = new SearchByEmpresaCmd(queryObject);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return ApplyPagination(result, queryObject);
            }
            else if (queryObject.ByIsActa != null)
            {
                var cmd = new SearchActaCmd(queryObject);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return ApplyPagination(result, queryObject);
            }
            else if (queryObject.ByIsRefrendado != null)
            {
                var cmd = new SearchByRefrendadoCmd(queryObject);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return ApplyPagination(result, queryObject);
            }
            else if (queryObject.ByEstado != null)
            {
                var cmd = new SearchByEstadoCmd(queryObject);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return ApplyPagination(result, queryObject);
            }
            else if (queryObject.ByArea != null)
            {
                var cmd = new SearchByAreasCmd(queryObject);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return ApplyPagination(result, queryObject);
            }
            else if (queryObject.ByFechaFirma != null)
            {
                var cmd = new SearchByFechaFirmaCmd(queryObject);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return ApplyPagination(result, queryObject);
            }
            else if (queryObject.ByFechaFin != null)
            {
                var cmd = new SearchByFechaFinCmd(queryObject);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return ApplyPagination(result, queryObject);
            }
            else if (queryObject.ByAntiguedadDto != null)
            {
                var cmd = new SearchByAntiguedadCmd(queryObject);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return ApplyPagination(result, queryObject);
            }
            else if (queryObject.ByProximosAvencer != null)
            {
                var cmd = new SearchProximosAvencerCmd(queryObject);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return ApplyPagination(result, queryObject);
            }
            else if (queryObject.ByMes != null)
            {
                var cmd = new SearchByMesCmd(queryObject);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return ApplyPagination(result, queryObject);
            }
            else if (queryObject.ByAnio != null)
            {
                var cmd = new SearchByAnioCmd(queryObject);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return ApplyPagination(result, queryObject);
            }
            else if (queryObject.ByDesdeHastaDto != null)
            {
                var cmd = new SearchByDesdeHastaCmd(queryObject);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return ApplyPagination(result, queryObject);
            }
            else if (queryObject.CountFirmadosByMesDto != null)
            {
                var cmd = new CountConvFirmadosByMesCmd(queryObject.CountFirmadosByMesDto);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return ApplyPagination(result, queryObject);
            }
            else if (queryObject.countFirmadosByRangoDto != null)
            {
                var cmd = new CountConvFirmadosByRangoCmd(queryObject.countFirmadosByRangoDto);
                var result = await cmd.ExecuteAsync(_UnitOfWork);
                return ApplyPagination(result, queryObject);
            }


            return Result<object>.Error("Porfavor seleccione un filtro", 400);
        }

        private Result<object> ApplyPagination(Result<object> result, ConvenioQueryObject query)
        {
            if (!result.Exit || result.Data == null || result is PaginatedResult<object>) return result;

            int skip = (query.PaginaActual - 1) * query.CantidadResultados;
            int take = query.CantidadResultados;

            if (result.Data is ListConveniosDto ambos)
            {
                var totalMarcos = ambos.conveniosMarcos.Count();
                var totalEspecificos = ambos.convenioEspecificos.Count();
                var maxTotal = System.Math.Max(totalMarcos, totalEspecificos);

                ambos.conveniosMarcos = ambos.conveniosMarcos.Skip(skip).Take(take).ToList();
                ambos.convenioEspecificos = ambos.convenioEspecificos.Skip(skip).Take(take).ToList();

                return PaginatedResult<object>.ExitoPaginado(ambos, maxTotal, query.PaginaActual, take);
            }
            else if (result.Data is IEnumerable<APIconvenios.DTOs.ConvenioMarco.ConvenioMarcoDto> marcos)
            {
                var total = marcos.Count();
                var paged = marcos.Skip(skip).Take(take).ToList();
                return PaginatedResult<object>.ExitoPaginado(paged, total, query.PaginaActual, take);
            }
            else if (result.Data is IEnumerable<APIconvenios.DTOs.ConvenioEspecifico.ConvenioEspecificoDto> especificos)
            {
                var total = especificos.Count();
                var paged = especificos.Skip(skip).Take(take).ToList();
                return PaginatedResult<object>.ExitoPaginado(paged, total, query.PaginaActual, take);
            }

            return result;
        }
    }
}
