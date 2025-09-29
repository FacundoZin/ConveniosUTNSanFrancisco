using APIconvenios.Common;
using APIconvenios.DTOs.Filters;
using APIconvenios.Helpers.Mappers;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Commands.FilterCommands.Commands
{
    public class SearchByTitleCmd : IFilterCommands
    {
        private readonly ByTitleDto _dto;
        public SearchByTitleCmd(ByTitleDto dto)
        {
            _dto = dto;
        }
        public async Task<Result<object>> ExecuteAsync(_UnitOfWork _UnitOfWork)
        {
            if(_dto.ConvenioType.Type == "especifico")
            {
                var query = _UnitOfWork._ConvenioEspecificoRepository.GetQuery();

                var Convenios = await query.Where(c => c.TituloConvenio.Contains(
                    _dto.Title, StringComparison.OrdinalIgnoreCase)).ToListAsync();

                if (Convenios.Count == 0) return Result<object>.
                        Error("No se encontraron convenios con ese título", 404);

                return Result<object>.Exito(Convenios.ToDto());
            }
            else
            {
                var query = _UnitOfWork._ConvenioMarcoRepository.GetQuery();

                var Convenios = await query.Where(c => c.Titulo.Contains(
                    _dto.Title, StringComparison.OrdinalIgnoreCase)).ToListAsync();

                if (Convenios.Count == 0) return Result<object>.
                        Error("No se encontraron convenios con ese título", 404);

                return Result<object>.Exito(Convenios.ToDto());
            }
        }
    }
}
