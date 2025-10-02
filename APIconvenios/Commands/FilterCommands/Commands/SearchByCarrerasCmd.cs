using APIconvenios.Common;
using APIconvenios.DTOs.Filters;
using APIconvenios.Helpers.Mappers;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Commands.FilterCommands.Commands
{
    public class SearchByCarrerasCmd : IFilterCommands
    {
        private readonly ByCarreraInvolucradaDto _Dto;
        public SearchByCarrerasCmd(ByCarreraInvolucradaDto dto)
        {
            _Dto = dto;
        }

        public async Task<Result<object>> ExecuteAsync(_UnitOfWork _UnitOfWork)
        {
           var query = _UnitOfWork._ConvenioEspecificoRepository.GetQuery();

            var convenios = await query.Where(convenio => convenio.CarrerasInvolucradas != null
            && convenio.CarrerasInvolucradas.Any(carrera => carrera.Nombre.ToLower() == _Dto.nombreCarrera.ToLower()))
                .Include(c => c.empresa)
                .ToListAsync();

            if (convenios.Count == 0) return Result<object>.Error("no se encontraron convenios asociados a la carrera seleccionada", 404);

            return Result<object>.Exito(convenios.ToDto());
        }
    }
}
