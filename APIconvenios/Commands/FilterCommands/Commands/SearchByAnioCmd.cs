using APIconvenios.Common;
using APIconvenios.DTOs.Convenios;
using APIconvenios.DTOs.Filters;
using APIconvenios.Helpers.Mappers;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Commands.FilterCommands.Commands
{
    public class SearchByAnioCmd : IFilterCommands
    {
        private readonly ByAnioDto _byAnioDto;

        public SearchByAnioCmd(ByAnioDto byAnioDto)
        {
            _byAnioDto = byAnioDto;
        }   
        public async Task<Result<object>> ExecuteAsync(_UnitOfWork _UnitOfWork)
        {
            if(_byAnioDto.convenioType == "marco")
            {
                var query = _UnitOfWork._ConvenioMarcoRepository.GetQueryByFiltering();

                var convenios = await query.Where(c => c.FechaFirmaConvenio.Value.Year == _byAnioDto.year).ToListAsync();

                return Result<object>.Exito(convenios.ToDto());
            }
            else if(_byAnioDto.convenioType == "especifico")
            {
                var query = _UnitOfWork._ConvenioEspecificoRepository.GetQueryByFiltering();

                var convenios = await query.Where(c => c.FechaFirmaConvenio.Value.Year == _byAnioDto.year).ToListAsync();

                return Result<object>.Exito(convenios.ToDto());
            }
            else
            {
                var context1 = await _UnitOfWork._ContextFactory.CreateDbContextAsync();
                var context2 = await _UnitOfWork._ContextFactory.CreateDbContextAsync();

                var task1 = context1.ConveniosEspecificos.Where(c => c.FechaFirmaConvenio.Value.Year == _byAnioDto.year).ToListAsync();

                var task2 = context2.ConveniosMarcos.Where(c => c.FechaFirmaConvenio.Value.Year == _byAnioDto.year).ToListAsync();

                await Task.WhenAll(task1, task2);

                var conveniosEspecificos = await task1;
                var conveniosMarcos = await task2;

                if (conveniosMarcos.Count == 0 && conveniosEspecificos.Count == 0)
                    return Result<object>.Error("no hay convenios que coincidan con la busqueda", 404);


                var Data = new ListConveniosDto
                {
                    conveniosMarcos = conveniosMarcos.ToDto(),
                    convenioEspecificos = conveniosEspecificos.ToDto(),
                };


                return Result<object>.Exito(Data);
            }
        }
    }
}
