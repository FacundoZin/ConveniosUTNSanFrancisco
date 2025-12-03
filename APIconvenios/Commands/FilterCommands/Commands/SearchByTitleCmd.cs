using APIconvenios.Common;
using APIconvenios.DTOs.Convenios;
using APIconvenios.DTOs.Filters;
using APIconvenios.Helpers.Mappers;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
            if(_dto.convenioType == "especifico")
            {
                var query = _UnitOfWork._ConvenioEspecificoRepository.GetQueryByFiltering();

                var Convenios = await query.Where(c => c.TituloConvenio != null && c.TituloConvenio.ToLower().Contains(
                    _dto.Title.ToLower())).ToListAsync();

                if (Convenios.Count == 0) return Result<object>.
                        Error("No se encontraron convenios con ese título", 404);

                return Result<object>.Exito(Convenios.ToDto());
            }
            else if(_dto.convenioType == "marco")
            {
                var query = _UnitOfWork._ConvenioMarcoRepository.GetQueryByFiltering();

                var Convenios = await query.Where(c => c.Titulo != null && c.Titulo.ToLower().Contains(
                    _dto.Title.ToLower())).ToListAsync();

                if (Convenios.Count == 0) return Result<object>.
                        Error("No se encontraron convenios con ese título", 404);

                return Result<object>.Exito(Convenios.ToDto());
            }
            else
            {
                var context1 = await _UnitOfWork._ContextFactory.CreateDbContextAsync();
                var context2 = await _UnitOfWork._ContextFactory.CreateDbContextAsync();

                var task1 = context1.ConveniosEspecificos.Where(c => c.TituloConvenio != null && c.TituloConvenio.ToLower().Contains(
                    _dto.Title.ToLower())).ToListAsync();

                var task2 = context2.ConveniosMarcos.Where(c => c.Titulo != null && c.Titulo.ToLower().Contains(
                    _dto.Title.ToLower())).ToListAsync();

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
