using APIconvenios.Common;
using APIconvenios.DTOs.Filters;
using APIconvenios.Helpers.Mappers;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Commands.FilterCommands.Commands
{
    public class SearchByRefrendadoCmd : IFilterCommands
    {
        private readonly ByRefrendadoDto _Dto;
        public SearchByRefrendadoCmd(ByRefrendadoDto dto)
        {
            _Dto = dto;
        }

        public async Task<Result<object>> ExecuteAsync(_UnitOfWork _UnitOfWork)
        {
            if(_Dto.convenioType.Type == "marco")
            {
                var query = _UnitOfWork._ConvenioMarcoRepository.GetQuery();
                var Convenios = await query.Where(c => c.Refrendado == true).ToListAsync();

                if(Convenios.Count == 0) return Result<object>.Error("No se encontraron convenios refrendados", 404);

                return Result<object>.Exito(Convenios.ToDto());
            }
            else
            {
                var query = _UnitOfWork._ConvenioEspecificoRepository.GetQuery();
                var Convenios = await query.Where(c => c.Refrendado == true).ToListAsync();

                if (Convenios.Count == 0) return Result<object>.Error("No se encontraron convenios refrendados", 404);

                return Result<object>.Exito(Convenios.ToDto());
            }
        }
    }
}
