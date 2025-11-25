using APIconvenios.Common;
using APIconvenios.DTOs.Filters;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Commands.FilterCommands.Commands
{
    public class CountConvFirmadosByMesCmd : IFilterCommands
    {
        private readonly CountConvFirmadosByMesDto _dto;

        public CountConvFirmadosByMesCmd(CountConvFirmadosByMesDto dto)
        {
            _dto = dto;
        }

        public async Task<Result<object>> ExecuteAsync(_UnitOfWork _UnitOfWork)
        {
            if (_dto.convenioType == "marco")
            {
                var query = _UnitOfWork._ConvenioMarcoRepository.GetQuery();

                int cantidad = await query.Where(c => c.FechaFirmaConvenio.Value.Year == _dto.year &&
                    c.FechaFirmaConvenio.Value.Month == _dto.month)
                    .CountAsync();

                return Result<object>.Exito(cantidad);
            }
            else
            {
                var query = _UnitOfWork._ConvenioEspecificoRepository.GetQuery();

                int cantidad = await query.Where(c => c.FechaFirmaConvenio.Value.Year == _dto.year &&
                    c.FechaFirmaConvenio.Value.Month == _dto.month)
                    .CountAsync();

                return Result<object>.Exito(cantidad);
            }
        }
    }
}
