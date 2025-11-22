using APIconvenios.Common;
using APIconvenios.DTOs.Filters;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Commands.FilterCommands.Commands
{
    public class SearchByAñoCmd : IFilterCommands
    {
        private readonly ByAñoDto _byAñoDto;

        public SearchByAñoCmd(ByAñoDto byAñoDto)
        {
            _byAñoDto = byAñoDto;
        }   
        public async Task<Result<object>> ExecuteAsync(_UnitOfWork _UnitOfWork)
        {
            if(_byAñoDto.convenioType == "marco")
            {
                var query = _UnitOfWork._ConvenioMarcoRepository.GetQuery();

                var convenios = await query.Where(c => c.FechaFirmaConvenio.Value.Year == _byAñoDto.year).ToListAsync();

                return Result<object>.Exito(convenios);
            }
            else
            {
                var query = _UnitOfWork._ConvenioEspecificoRepository.GetQuery();

                var convenios = await query.Where(c => c.FechaFirmaConvenio.Value.Year == _byAñoDto.year).ToListAsync();

                return Result<object>.Exito(convenios);
            }
        }
    }
}
