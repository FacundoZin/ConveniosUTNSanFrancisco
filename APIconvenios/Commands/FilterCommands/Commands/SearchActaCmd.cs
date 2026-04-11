using APIconvenios.Common;
using APIconvenios.DTOs.Filters;
using APIconvenios.Helpers.Mappers;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Commands.FilterCommands.Commands
{
    public class SearchActaCmd : IFilterCommands
    {
        private readonly ConvenioQueryObject _query;
        private readonly ByIsActaDto _Dto;
        public SearchActaCmd(ConvenioQueryObject query)
        {
            _query = query;
            _Dto = query.ByIsActa;
        }
        public async Task<Result<object>> ExecuteAsync(_UnitOfWork _UnitOfWork)
        {
            var query = _UnitOfWork._ConvenioEspecificoRepository.GetQueryByFiltering()
                .Where(c => c.EsActa == true);

            int total = await query.CountAsync();
            var convenios = await query
                .Skip((_query.PaginaActual - 1) * _query.CantidadResultados)
                .Take(_query.CantidadResultados)
                .ToListAsync();

            if(total == 0) return Result<object>.Error("No se encontraron convenios especificos de tipo acta", 404);

            return PaginatedResult<object>.ExitoPaginado(convenios.ToDto(), total, _query.PaginaActual, _query.CantidadResultados);
        }
    }
}
