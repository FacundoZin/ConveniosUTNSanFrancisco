using APIconvenios.Common;
using APIconvenios.DTOs.Convenios;
using APIconvenios.DTOs.Filters;
using APIconvenios.Helpers.Mappers;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Windows.Input;

namespace APIconvenios.Commands.FilterCommands.Commands
{
    public class SearchByNumeroResolucionCmd : IFilterCommands
    {
        private readonly ByNumeroResolucionDto _dto;
        public SearchByNumeroResolucionCmd(ByNumeroResolucionDto dto)
        {
            _dto = dto;
        }
        public async Task<Result<object>> ExecuteAsync(_UnitOfWork _UnitOfWork)
        {
            if (_dto.convenioType == "marco")
            {
                var query = _UnitOfWork._ConvenioMarcoRepository.GetQueryByFiltering();
                var Convenios = await query.Where(c => c.NumeroResolucion == _dto.NumeroResolucion)
                    .ToListAsync();

                if (Convenios.Count == 0) return Result<object>.Error("convenio no encontrado", 404);

                return Result<object>.Exito(Convenios.ToDto());
            }
            else if(_dto.convenioType == "especifico")
            {
                var query = _UnitOfWork._ConvenioEspecificoRepository.GetQueryByFiltering();
                var Convenios = await query.Where(c => c.NumeroResolucion == _dto.NumeroResolucion)
                    .ToListAsync();

                if (Convenios.Count == 0) return Result<object>.Error("convenio  no encontrado", 404);

                return Result<object>.Exito(Convenios.ToDto());
            }
            else
            {
                var context1 = await _UnitOfWork._ContextFactory.CreateDbContextAsync();
                var context2 = await _UnitOfWork._ContextFactory.CreateDbContextAsync();

                var task1 = context1.ConveniosEspecificos.Where(c => c.NumeroResolucion == _dto.NumeroResolucion)
                    .ToListAsync();

                var task2 = context2.ConveniosMarcos.Where(c => c.NumeroResolucion == _dto.NumeroResolucion)
                    .ToListAsync();

                await Task.WhenAll(task1, task2);

                var conveniosEspecificos = await task1;
                var conveniosMarcos = await task2;

                if (conveniosMarcos.Count == 0 && conveniosEspecificos.Count == 0)
                    return Result<object>.Error("no hay convenios que coincidan con la busqueda", 404);


                var Data = new ListConveniosDto
                {
                    conveniosMarcos = conveniosMarcos.ToDto(),
                    convenioEspecificos = conveniosEspecificos.ToDto(),
                };


                return Result<object>.Exito(Data);
            }
        }
    }
}
