using APIconvenios.Common;
using APIconvenios.DTOs.Filters;
using APIconvenios.Helpers.Mappers;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Commands.FilterCommands.Commands
{
    public class SearchActaCmd : IFilterCommands
    {
        private readonly ByIsActaDto _Dto;
        public SearchActaCmd(ByIsActaDto dto)
        {
            _Dto = dto;
        }
        public async Task<Result<object>> ExecuteAsync(_UnitOfWork _UnitOfWork)
        {
            var query = _UnitOfWork._ConvenioEspecificoRepository.GetQuery();

            var convenios = await query.Where(c => c.EsActa == true).Include(c => c.empresa).ToListAsync();

            if(convenios.Count == 0) return Result<object>.Error("No se encontraron convenios especificos de tipo acta", 404);

            return Result<object>.Exito(convenios.ToDto());
        }
    }
}
