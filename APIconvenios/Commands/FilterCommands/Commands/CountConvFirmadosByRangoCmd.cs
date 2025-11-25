using APIconvenios.Common;
using APIconvenios.DTOs.Filters;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Commands.FilterCommands.Commands
{
    public class CountConvFirmadosByRangoCmd : IFilterCommands
    {
        private readonly CountConveniosFirmadosByRangoDto _dto;
        public CountConvFirmadosByRangoCmd(CountConveniosFirmadosByRangoDto dto)
        {
            _dto = dto;
        }

        public async Task<Result<object>> ExecuteAsync(_UnitOfWork _UnitOfWork)
        {
            if (_dto.convenioType == "marco")
            {
                var query = _UnitOfWork._ConvenioMarcoRepository.GetQuery();

                int cantidad = await query.Where(c => c.FechaFirmaConvenio.HasValue
                && c.FechaFirmaConvenio.Value >= _dto.desde
                && c.FechaFirmaConvenio.Value <= _dto.hasta).CountAsync();

                return Result<object>.Exito(cantidad);
            }
            else
            {
                var query = _UnitOfWork._ConvenioEspecificoRepository.GetQuery();

                int cantidad = await query.Where(c => c.FechaFirmaConvenio.HasValue
                && c.FechaFirmaConvenio.Value >= _dto.desde
                && c.FechaFirmaConvenio.Value <= _dto.hasta).CountAsync();


                return Result<object>.Exito(cantidad);
            }
        }
    }
}
