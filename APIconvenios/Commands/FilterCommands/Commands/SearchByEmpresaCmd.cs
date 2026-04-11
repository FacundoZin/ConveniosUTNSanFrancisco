using APIconvenios.Common;
using APIconvenios.DTOs.Convenios;
using APIconvenios.DTOs.Filters;
using APIconvenios.Helpers.Mappers;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Commands.FilterCommands.Commands
{
    public class SearchByEmpresaCmd : IFilterCommands
    {
        private readonly ConvenioQueryObject _query;
        private readonly ByEmpresaDto _Dto;
        public SearchByEmpresaCmd(ConvenioQueryObject query)
        {
            _query = query;
            _Dto = query.ByEmpresa;
        }

        public async Task<Result<object>> ExecuteAsync(_UnitOfWork _UnitOfWork)
        {
            if (_Dto.convenioType == "marco")
            {
                var query = _UnitOfWork._ConvenioMarcoRepository.GetQueryByFiltering()
                    .Where(c => c.Empresa != null && c.Empresa.Nombre.ToLower().Contains(_Dto.EmpresaName.ToLower()));

                int total = await query.CountAsync();

                var convenios = await query
                    .Skip((_query.PaginaActual - 1) * _query.CantidadResultados)
                    .Take(_query.CantidadResultados)
                    .ToListAsync();

                if (total == 0) return Result<object>.Error("No se encontraron convenios con la empresa especificada.", 404);

                return PaginatedResult<object>.ExitoPaginado(convenios.ToDto(), total, _query.PaginaActual, _query.CantidadResultados);
            }
            else if(_Dto.convenioType == "especifico")
            {
                var query = _UnitOfWork._ConvenioEspecificoRepository.GetQueryByFiltering()
                    .Where(c => c.empresa != null && c.empresa.Nombre.ToLower().Contains(_Dto.EmpresaName.ToLower()));

                int total = await query.CountAsync();

                var convenios = await query
                    .Skip((_query.PaginaActual - 1) * _query.CantidadResultados)
                    .Take(_query.CantidadResultados)
                    .ToListAsync();

                if (total == 0) return Result<object>.Error("No se encontraron convenios con la empresa especificada.", 404);

                return PaginatedResult<object>.ExitoPaginado(convenios.ToDto(), total, _query.PaginaActual, _query.CantidadResultados);
            }
            else
            {
                var context1 = await _UnitOfWork._ContextFactory.CreateDbContextAsync();
                var context2 = await _UnitOfWork._ContextFactory.CreateDbContextAsync();

                var q1 = context1.ConveniosEspecificos.Where(c => c.empresa != null && c.empresa.Nombre.ToLower().Contains(_Dto.EmpresaName.ToLower()));
                var q2 = context2.ConveniosMarcos.Where(c => c.Empresa != null && c.Empresa.Nombre.ToLower().Contains(_Dto.EmpresaName.ToLower()));

                var taskTotal1 = q1.CountAsync();
                var taskTotal2 = q2.CountAsync();
                await Task.WhenAll(taskTotal1, taskTotal2);
                
                int maxTotal = System.Math.Max(taskTotal2.Result, taskTotal1.Result);

                if (maxTotal == 0)
                    return Result<object>.Error("no hay convenios que coincidan con la busqueda", 404);

                int skip = (_query.PaginaActual - 1) * _query.CantidadResultados;
                int take = _query.CantidadResultados;

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
