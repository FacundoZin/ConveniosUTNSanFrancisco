using APIconvenios.Common;
using APIconvenios.DTOs.Convenios;
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
                var query = _UnitOfWork._ConvenioMarcoRepository.GetQueryByFiltering();

                int cantidad = await query.Where(c => c.FechaFirmaConvenio.HasValue
                && c.FechaFirmaConvenio.Value >= _dto.desde
                && c.FechaFirmaConvenio.Value <= _dto.hasta).CountAsync();

                return Result<object>.Exito(cantidad);
            }
            else if(_dto.convenioType == "especifico")
            {
                var query = _UnitOfWork._ConvenioEspecificoRepository.GetQueryByFiltering();

                int cantidad = await query.Where(c => c.FechaFirmaConvenio.HasValue
                && c.FechaFirmaConvenio.Value >= _dto.desde
                && c.FechaFirmaConvenio.Value <= _dto.hasta).CountAsync();


                return Result<object>.Exito(cantidad);
            }
            else
            {
                var context1 = await _UnitOfWork._ContextFactory.CreateDbContextAsync();
                var context2 = await _UnitOfWork._ContextFactory.CreateDbContextAsync();

                var task1 = context1.ConveniosEspecificos.Where(c => c.FechaFirmaConvenio.HasValue
                && c.FechaFirmaConvenio.Value >= _dto.desde
                && c.FechaFirmaConvenio.Value <= _dto.hasta).CountAsync();

                var task2 = context2.ConveniosMarcos.Where(c => c.FechaFirmaConvenio.HasValue
                && c.FechaFirmaConvenio.Value >= _dto.desde
                && c.FechaFirmaConvenio.Value <= _dto.hasta).CountAsync();

                await Task.WhenAll(task1, task2);

                var cantidadEspecificos = await task1;
                var cantidadMarcos = await task2;

                var cantidad = new CantidadConveniosDto
                {
                    cantidadEspecificos = cantidadEspecificos,
                    cantidadMarcos = cantidadMarcos
                };

                return Result<object>.Exito(cantidad);
            }
        }
    }
}
