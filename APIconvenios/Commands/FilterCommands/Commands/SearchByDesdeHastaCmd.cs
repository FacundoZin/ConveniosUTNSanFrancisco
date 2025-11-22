using APIconvenios.Common;
using APIconvenios.DTOs.Filters;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Commands.FilterCommands.Commands
{
    public class SearchByDesdeHastaCmd : IFilterCommands
    {
        private readonly ByDesdeHastaDto _Dto;
        public SearchByDesdeHastaCmd(ByDesdeHastaDto dto)
        {
            _Dto = dto;
        }

        public async Task<Result<object>> ExecuteAsync(_UnitOfWork _UnitOfWork)
        {
            if(_Dto.convenioType == "marco")
            {
                var query = _UnitOfWork._ConvenioMarcoRepository.GetQuery();

                var convenios = await query.Where(c => c.FechaFirmaConvenio.HasValue
                && c.FechaFirmaConvenio.Value >= _Dto.desde
                && c.FechaFirmaConvenio.Value <= _Dto.hasta).ToListAsync();

                return Result<object>.Exito(convenios);
            }
            else
            {
                var query = _UnitOfWork._ConvenioEspecificoRepository.GetQuery();

                var convenios = await query.Where(c => c.FechaFirmaConvenio.HasValue
                && c.FechaFirmaConvenio.Value >= _Dto.desde
                && c.FechaFirmaConvenio.Value <= _Dto.hasta).ToListAsync();

                return Result<object>.Exito(convenios);
            }
        }
    }
}
