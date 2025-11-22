using APIconvenios.Common;
using APIconvenios.DTOs.Filters;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Commands.FilterCommands.Commands
{
    public class SearchByMesCmd : IFilterCommands
    {
        private readonly ByMesDto _byMesDto;
        public SearchByMesCmd(ByMesDto byMesDto)
        {
            _byMesDto = byMesDto;
        }

        public async Task<Result<object>> ExecuteAsync(_UnitOfWork _UnitOfWork)
        {
            if ( _byMesDto.convenioType == "marco")
            {
                var query = _UnitOfWork._ConvenioMarcoRepository.GetQuery();
                var convenios = await query.Where(c => c.FechaFirmaConvenio.Value.Year == _byMesDto.year && 
                    c.FechaFirmaConvenio.Value.Month == _byMesDto.month)
                    .ToListAsync();

                return Result<object>.Exito(convenios);
            }
            else
            {
                var query = _UnitOfWork._ConvenioEspecificoRepository.GetQuery();
                var convenios = await query.Where(c => c.FechaFirmaConvenio.Value.Year == _byMesDto.year && 
                    c.FechaFirmaConvenio.Value.Month == _byMesDto.month)
                    .ToListAsync();

                return Result<object>.Exito(convenios); 
            }
        }
    }
}
