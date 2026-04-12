using APIconvenios.Common;
using APIconvenios.DTOs.Filters;
using APIconvenios.Helpers.Mappers;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Commands.FilterCommands.Commands
{
    public class SearchByAreasCmd : IFilterCommands
    {
        private readonly ConvenioQueryObject _query;
        private readonly ByAreaInvolucradaDto _Dto;
        public SearchByAreasCmd(ConvenioQueryObject query)
        {
            _query = query;
            _Dto = query.ByArea!;
        }

        public async Task<Result<object>> ExecuteAsync(_UnitOfWork _UnitOfWork)
        {
           var query = _UnitOfWork._ConvenioEspecificoRepository.GetQueryByFiltering()
                .Where(convenio => convenio.CarrerasInvolucradas.Any(carrera => carrera.Nombre == _Dto.nombreArea));

            int total = await query.CountAsync();
            var convenios = await query
                .Skip((_query.PaginaActual - 1) * _query.CantidadResultados)
                .Take(_query.CantidadResultados)
                .ToListAsync();

            if (total == 0) return Result<object>.Error("no se encontraron convenios asociados al área seleccionada", 404);

            return PaginatedResult<object>.ExitoPaginado(convenios.ToDto(), total, _query.PaginaActual, _query.CantidadResultados);
        }
    }
}
