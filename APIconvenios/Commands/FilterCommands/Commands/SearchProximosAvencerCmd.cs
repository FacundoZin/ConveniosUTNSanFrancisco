using APIconvenios.Common;
using APIconvenios.DTOs.Convenios;
using APIconvenios.DTOs.Filters;
using APIconvenios.Helpers.Mappers;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Commands.FilterCommands.Commands
{
    public class SearchProximosAvencerCmd : IFilterCommands
    {
        private readonly ConvenioQueryObject _query;
        private readonly ByProximosAvencerDto _Dto;
        public SearchProximosAvencerCmd(ConvenioQueryObject query)
        {
            _query = query;
            _Dto = query.ByProximosAvencer;
        }

        public async Task<Result<object>> ExecuteAsync(_UnitOfWork _UnitOfWork)
        {
            if (_Dto.convenioType == "marco")
            {
                var query = _UnitOfWork._ConvenioMarcoRepository.GetQueryByFiltering()
                    .Where(c => c.FechaFin != null).AsNoTracking();

                int total = await query.CountAsync();
                var convenios = await query
                    .OrderByDescending(c => c.FechaFin)
                    .Skip((_query.PaginaActual - 1) * _query.CantidadResultados)
                    .Take(_query.CantidadResultados)
                    .ToListAsync();

                if (total == 0) return Result<object>.
                        Error("no hay convenios marcos registrados", 404);

                return PaginatedResult<object>.ExitoPaginado(convenios.ToDto(), total, _query.PaginaActual, _query.CantidadResultados);
            }
            else if (_Dto.convenioType == "especifico")
            {
                var query = _UnitOfWork._ConvenioEspecificoRepository.GetQueryByFiltering()
                    .Where(c => c.FechaFinConvenio != null).AsNoTracking();

                int total = await query.CountAsync();
                var convenios = await query
                    .OrderByDescending(c => c.FechaFinConvenio)
                    .Skip((_query.PaginaActual - 1) * _query.CantidadResultados)
                    .Take(_query.CantidadResultados)
                    .ToListAsync();

                if (total == 0) return Result<object>.
                        Error("no hay convenios especificos registrados", 404);

                return PaginatedResult<object>.ExitoPaginado(convenios.ToDto(), total, _query.PaginaActual, _query.CantidadResultados);
            }
            else
            {
                var context1 = _UnitOfWork._ContextFactory.CreateDbContext();
                var context2 = _UnitOfWork._ContextFactory.CreateDbContext();

                var q1 = context1.ConveniosMarcos.Where(c => c.FechaFin != null);
                var q2 = context2.ConveniosEspecificos.Where(c => c.FechaFinConvenio != null);

                var taskTotal1 = q1.CountAsync();
                var taskTotal2 = q2.CountAsync();
                await Task.WhenAll(taskTotal1, taskTotal2);
                
                int maxTotal = System.Math.Max(taskTotal1.Result, taskTotal2.Result);

                if (maxTotal == 0) 
                    return Result<object>.Error("no hay convenios que coincidan con la busqueda", 404);

                int skip = (_query.PaginaActual - 1) * _query.CantidadResultados;
                int take = _query.CantidadResultados;

                var Task1 = q1.OrderByDescending(c => c.FechaFin).Skip(skip).Take(take).ToListAsync();
                var task2 = q2.OrderByDescending(c => c.FechaFinConvenio).Skip(skip).Take(take).ToListAsync();

                await Task.WhenAll(Task1, task2);

                var conveniosMarco = await Task1;
                var conveniosEspecificos = await task2;

                var Data = new ListConveniosDto
                {
                    conveniosMarcos = conveniosMarco.ToDto(),
                    convenioEspecificos = conveniosEspecificos.ToDto(),
                };

                return PaginatedResult<object>.ExitoPaginado(Data, maxTotal, _query.PaginaActual, take);
            }
        }
    }
}
