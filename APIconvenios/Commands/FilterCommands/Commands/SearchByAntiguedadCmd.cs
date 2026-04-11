using APIconvenios.Common;
using APIconvenios.DTOs.Convenios;
using APIconvenios.DTOs.Filters;
using APIconvenios.Helpers.Mappers;
using APIconvenios.Models;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Commands.FilterCommands.Commands
{
    public class SearchByAntiguedadCmd : IFilterCommands
    {
        private readonly ConvenioQueryObject _query;
        private readonly ByAntiguedadDto _Dto;

        public SearchByAntiguedadCmd(ConvenioQueryObject query)
        {
            _query = query;
            _Dto = query.ByAntiguedadDto;
        }

        public async Task<Result<object>> ExecuteAsync(_UnitOfWork _UnitOfWork)
        {
            if (_Dto.convenioType == "marco")
            {
                var queryBase = _UnitOfWork._ConvenioMarcoRepository.GetQueryByFiltering()
                    .Where(c => c.FechaFirmaConvenio != null);

                int total = await queryBase.CountAsync();
                
                IQueryable<APIconvenios.Models.ConvenioMarco> query;
                if (_Dto.ascendente)
                    query = queryBase.OrderBy(c => c.FechaFirmaConvenio);
                else
                    query = queryBase.OrderByDescending(c => c.FechaFirmaConvenio);

                var convenios = await query
                    .Skip((_query.PaginaActual - 1) * _query.CantidadResultados)
                    .Take(_query.CantidadResultados)
                    .ToListAsync();

                if (total == 0) return Result<object>.Error("no hay convenios marcos registrados", 404);

                return PaginatedResult<object>.ExitoPaginado(convenios.ToDto(), total, _query.PaginaActual, _query.CantidadResultados);
            }
            else if(_Dto.convenioType == "especifico")
            {
                var queryBase = _UnitOfWork._ConvenioEspecificoRepository.GetQueryByFiltering()
                    .Where(c => c.FechaFirmaConvenio != null);

                int total = await queryBase.CountAsync();
                
                IQueryable<APIconvenios.Models.ConvenioEspecifico> query;
                if (_Dto.ascendente)
                    query = queryBase.OrderBy(c => c.FechaFirmaConvenio);
                else
                    query = queryBase.OrderByDescending(c => c.FechaFirmaConvenio);

                var convenios = await query
                    .Skip((_query.PaginaActual - 1) * _query.CantidadResultados)
                    .Take(_query.CantidadResultados)
                    .ToListAsync();

                if (total == 0) return Result<object>.Error("no hay convenios especificos registrados", 404);

                return PaginatedResult<object>.ExitoPaginado(convenios.ToDto(), total, _query.PaginaActual, _query.CantidadResultados);
            }
            else
            {
                var context1 = await _UnitOfWork._ContextFactory.CreateDbContextAsync();
                var context2 = await _UnitOfWork._ContextFactory.CreateDbContextAsync();

                var q1Base = context1.ConveniosEspecificos.Where(c => c.FechaFirmaConvenio != null);
                var q2Base = context2.ConveniosMarcos.Where(c => c.FechaFirmaConvenio != null);

                var taskTotal1 = q1Base.CountAsync();
                var taskTotal2 = q2Base.CountAsync();
                await Task.WhenAll(taskTotal1, taskTotal2);
                
                int maxTotal = System.Math.Max(taskTotal1.Result, taskTotal2.Result);

                if (maxTotal == 0)
                    return Result<object>.Error("no hay convenios que coincidan con la busqueda", 404);

                int skip = (_query.PaginaActual - 1) * _query.CantidadResultados;
                int take = _query.CantidadResultados;

                IQueryable<APIconvenios.Models.ConvenioEspecifico> q1;
                IQueryable<APIconvenios.Models.ConvenioMarco> q2;

                if (_Dto.ascendente)
                {
                    q1 = q1Base.OrderBy(c => c.FechaFirmaConvenio);
                    q2 = q2Base.OrderBy(c => c.FechaFirmaConvenio);
                }
                else
                {
                    q1 = q1Base.OrderByDescending(c => c.FechaFirmaConvenio);
                    q2 = q2Base.OrderByDescending(c => c.FechaFirmaConvenio);
                }

                var task1 = q1.Skip(skip).Take(take).ToListAsync();
                var task2 = q2.Skip(skip).Take(take).ToListAsync();

                await Task.WhenAll(task1, task2);

                var conveniosEspecificos = await task1;
                var conveniosMarcos = await task2;

                var Data = new ListConveniosDto
                {
                    conveniosMarcos = conveniosMarcos.ToDto(),
                    convenioEspecificos = conveniosEspecificos.ToDto(),
                };

                return PaginatedResult<object>.ExitoPaginado(Data, maxTotal, _query.PaginaActual, take);
            }
        }
    }
}
