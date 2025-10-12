using APIconvenios.Common;
using APIconvenios.DTOs.Filters;
using APIconvenios.Helpers.Mappers;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Windows.Input;

namespace APIconvenios.Commands.FilterCommands.Commands
{
    public class SearchByNumeroResolucionCmd : IFilterCommands
    {
        private readonly ByNumeroResolucionDto _dto;
        public SearchByNumeroResolucionCmd(ByNumeroResolucionDto dto)
        {
            _dto = dto;
        }
        public async Task<Result<object>> ExecuteAsync(_UnitOfWork _UnitOfWork)
        {
            if (_dto.convenioType == "marco")
            {
                var query = _UnitOfWork._ConvenioMarcoRepository.GetQuery();
                var Convenios = await query.Where(c => c.NumeroResolucion == _dto.NumeroResolucion)
                    .Include(c => c.Empresa).ToListAsync();

                if (Convenios.Count == 0) return Result<object>.Error("convenio no encontrado", 404);

                return Result<object>.Exito(Convenios.ToDto());
            }
            else
            {
                var query = _UnitOfWork._ConvenioEspecificoRepository.GetQuery();
                var Convenios = await query.Where(c => c.NumeroResolucion == _dto.NumeroResolucion)
                    .Include(c => c.empresa).ToListAsync();

                if (Convenios.Count == 0) return Result<object>.Error("convenio  no encontrado", 404);

                return Result<object>.Exito(Convenios.ToDto());
            }
        }
    }
}
