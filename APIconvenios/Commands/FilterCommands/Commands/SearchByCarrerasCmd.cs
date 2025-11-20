using APIconvenios.Common;
using APIconvenios.DTOs.Filters;
using APIconvenios.Helpers.Mappers;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            var convenios = await query.Where(convenio => convenio.CarrerasInvolucradas.Any(carrera => carrera.Nombre == _Dto.nombreCarrera))
                .Include(c => c.empresa)
                .AsNoTracking()
                .ToListAsync();


            if (convenios.Count == 0) return Result<object>.Error("no se encontraron convenios asociados a la carrera seleccionada", 404);

            return Result<object>.Exito(convenios.ToDto());
        }
    }
}
