using APIconvenios.Common;
using APIconvenios.DTOs.Filters;
using APIconvenios.Helpers.Mappers;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Commands.FilterCommands.Commands
{
    public class SearchByEstadoCmd : IFilterCommands
    {
        private readonly ByEstadoConvenioDto _Dto;

        public SearchByEstadoCmd(ByEstadoConvenioDto dto)
        {
            _Dto = dto;
        }
        public async Task<Result<object>> ExecuteAsync(_UnitOfWork _UnitOfWork)
        {
            if (_Dto.convenioType == "marco")
            {
                var query = _UnitOfWork._ConvenioMarcoRepository.GetQuery();

                var convenios = await query.Where(c => c.Estado == _Dto.Estado)
                    .Include(c => c.Empresa)
                    .ToListAsync();

                if (convenios.Count == 0) return Result<object>.Error("No se encontraron convenios con el estado especificado.", 404);

                return Result<object>.Exito(convenios.ToDto());
            }
            else
            {
                var query = _UnitOfWork._ConvenioEspecificoRepository.GetQuery();

                var convenios = await query.Where(c => c.Estado == _Dto.Estado)
                    .Include(c => c.empresa)
                    .ToListAsync();

                if (convenios.Count == 0) return Result<object>.Error("No se encontraron convenios con el estado especificado.", 404);

                return Result<object>.Exito(convenios.ToDto());
            }
        }
    }
}
