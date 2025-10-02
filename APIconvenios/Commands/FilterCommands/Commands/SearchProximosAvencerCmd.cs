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

                var convenios = await query
                    .Where(c => c.FechaFin != null)
                    .OrderByDescending(c => c.FechaFin).Take(30).Include(c => c.Empresa).ToListAsync();

                if (convenios.Count == 0) return Result<object>.
                        Error("no hay convenios marcos registrados", 404);

                return Result<object>.Exito(convenios.ToDto());
            }
            else
            {
                var query = _UnitOfWork._ConvenioEspecificoRepository.GetQuery();

                var convenios = await query
                    .Where(c => c.FechaFinConvenio != null)
                    .OrderByDescending(c => c.FechaFinConvenio).Take(30).Include(c => c.empresa).ToListAsync();

                if (convenios.Count == 0) return Result<object>.
                        Error("no hay convenios marcos registrados", 404);

                return Result<object>.Exito(convenios.ToDto());
            }
        }
    }
}
