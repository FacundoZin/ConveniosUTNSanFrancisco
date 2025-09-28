using APIconvenios.Common;
using APIconvenios.DTOs.Filters;
using APIconvenios.UnitOfWork;

namespace APIconvenios.Commands.FilterCommands.Commands
{
    public class SearchByCarrerasCmd : IFilterCommands
    {
        private readonly ByCarreraInvolucradaDto _Dto;
        public SearchByCarrerasCmd(ByCarreraInvolucradaDto dto)
        {
            _Dto = dto;
        }

        public Task<Result<object>> ExecuteAsync(_UnitOfWork _UnitOfWork)
        {
           var query = _UnitOfWork._ConvenioEspecificoRepository.GetQuery();

            var convenios = query.Where(c => c.CarrerasInvolucradas.Any());
        }
    }
}
