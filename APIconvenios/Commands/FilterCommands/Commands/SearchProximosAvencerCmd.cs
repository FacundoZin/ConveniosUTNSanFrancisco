using APIconvenios.Common;
using APIconvenios.DTOs.Filters;
using APIconvenios.Helpers.Mappers;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Commands.FilterCommands.Commands
{
    public class SearchProximosAvencerCmd : IFilterCommands
    {
        private readonly ByProximosAvencerDto _Dto;
        public SearchProximosAvencerCmd(ByProximosAvencerDto dto)
        {
            _Dto = dto;
        }

        public async Task<Result<object>> ExecuteAsync(_UnitOfWork _UnitOfWork)
        {
            if(_Dto.convenioType.Type == "marco")
            {
                var query = _UnitOfWork._ConvenioMarcoRepository.GetQuery();

                var convenios = await query.OrderByDescending(c => c.FechaFin).Take(30).ToListAsync();

                if (convenios.Count == 0) return Result<object>.
                        Error("no hay convenios marcos registrados", 404);

                return Result<object>.Exito(convenios.ToDto());
            }
            else
            {
                var query = _UnitOfWork._ConvenioEspecificoRepository.GetQuery();

                var convenios = await query.OrderByDescending(c => c.FechaFinConvenio).Take(30).ToListAsync();

                if (convenios.Count == 0) return Result<object>.
                        Error("no hay convenios marcos registrados", 404);

                return Result<object>.Exito(convenios.ToDto());
            }
        }
    }
}
