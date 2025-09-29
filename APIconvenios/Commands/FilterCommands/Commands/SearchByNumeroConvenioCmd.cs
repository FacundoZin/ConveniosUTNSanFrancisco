using APIconvenios.Common;
using APIconvenios.DTOs.Filters;
using APIconvenios.Helpers.Mappers;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Commands.FilterCommands.Commands
{
    public class SearchByNumeroConvenioCmd : IFilterCommands
    {
        private readonly ByNumeroConvenioDto _dto;
        public SearchByNumeroConvenioCmd(ByNumeroConvenioDto dto)
        {
            _dto = dto;
        }
        public async Task<Result<object>> ExecuteAsync(_UnitOfWork _UnitOfWork)
        {
            if (_dto.ConvenioType.Type == "marco")
            {
                var query = _UnitOfWork._ConvenioMarcoRepository.GetQuery();
                var Convenios = await query.Where(c => c.numeroconvenio.Contains(
                    _dto.NumeroConvenio, StringComparison.OrdinalIgnoreCase)).ToListAsync();

                if (Convenios.Count == 0) return Result<object>.Error("convenio no encontrado", 404);

                return Result<object>.Exito(Convenios.ToDto());
            }
            else
            {
                var query = _UnitOfWork._ConvenioEspecificoRepository.GetQuery();
                var Convenios = await query.Where(c => c.NumeroResolucion.Contains(
                    _dto.NumeroConvenio, StringComparison.OrdinalIgnoreCase)).ToListAsync();

                if (Convenios.Count == 0) return Result<object>.Error("convenio  no encontrado", 404);

                return Result<object>.Exito(Convenios.ToDto());
            }
        }
    }
}
