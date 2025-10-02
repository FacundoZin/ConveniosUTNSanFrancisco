using APIconvenios.Common;
using APIconvenios.DTOs.Filters;
using APIconvenios.Helpers.Mappers;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Commands.FilterCommands.Commands
{
    public class SearchByFechaFinCmd : IFilterCommands
    {
        private readonly ByFechaFinDto _Dto;
        public SearchByFechaFinCmd(ByFechaFinDto dto)
        {
            _Dto = dto;
        }

        public async Task<Result<object>> ExecuteAsync(_UnitOfWork _UnitOfWork)
        {
            if (_Dto.convenioType.Type == "marco")
            {
                var query = _UnitOfWork._ConvenioMarcoRepository.GetQuery();
                var convenios = await query.Where(c => c.FechaFin == _Dto.FechaFin)
                    .Include(c => c.Empresa).ToListAsync();

                if (convenios.Count == 0) return Result<object>.
                        Error("No se encontraron convenios marco con la fecha de finalizacion especificada.", 404);

                return Result<object>.Exito(convenios.ToDto());
            }
            else
            {
                var query = _UnitOfWork._ConvenioEspecificoRepository.GetQuery();
                var convenios = await query.Where(c => c.FechaFinConvenio == _Dto.FechaFin)
                    .Include(c => c.empresa).ToListAsync();

                if (convenios.Count == 0) return Result<object>.
                        Error("No se encontraron convenios especificos con la fecha de finalizacion especificada.", 404);

                return Result<object>.Exito(convenios.ToDto());
            }
        }
    }
}
