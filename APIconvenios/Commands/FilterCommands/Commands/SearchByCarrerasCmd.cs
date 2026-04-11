using APIconvenios.Common;
using APIconvenios.DTOs.Filters;
using APIconvenios.Helpers.Mappers;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace APIconvenios.Commands.FilterCommands.Commands
{
    public class SearchByCarrerasCmd : IFilterCommands
    {
        private readonly ConvenioQueryObject _query;
        private readonly ByCarreraInvolucradaDto _Dto;
        public SearchByCarrerasCmd(ConvenioQueryObject query)
        {
            _query = query;
            _Dto = query.ByCarrera;
        }

        public async Task<Result<object>> ExecuteAsync(_UnitOfWork _UnitOfWork)
        {
           var query = _UnitOfWork._ConvenioEspecificoRepository.GetQueryByFiltering()
                .Where(convenio => convenio.CarrerasInvolucradas.Any(carrera => carrera.Nombre == _Dto.nombreCarrera));

            int total = await query.CountAsync();
            var convenios = await query
                .Skip((_query.PaginaActual - 1) * _query.CantidadResultados)
                .Take(_query.CantidadResultados)
                .ToListAsync();

            if (total == 0) return Result<object>.Error("no se encontraron convenios asociados a la carrera seleccionada", 404);

            return PaginatedResult<object>.ExitoPaginado(convenios.ToDto(), total, _query.PaginaActual, _query.CantidadResultados);
        }
    }
}
