using APIconvenios.Common;
using APIconvenios.DTOs.Convenios;
using APIconvenios.DTOs.Filters;
using APIconvenios.Helpers.Mappers;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Commands.FilterCommands.Commands
{
    public class SearchProximosAvencerCmd : IFilterCommands
    {
        private readonly ByProximosAvencerDto _Dto;
        public SearchProximosAvencerCmd(ByProximosAvencerDto dto)
        {
            _Dto = dto;
        }

        public async Task<Result<object>> ExecuteAsync(_UnitOfWork _UnitOfWork)
        {
            if (_Dto.convenioType == "marco")
            {
                var query = _UnitOfWork._ConvenioMarcoRepository.GetQueryByFiltering();

                var convenios = await query
                    .Where(c => c.FechaFin != null).AsNoTracking()
                    .OrderByDescending(c => c.FechaFin).Take(30).ToListAsync();

                if (convenios.Count == 0) return Result<object>.
                        Error("no hay convenios marcos registrados", 404);

                return Result<object>.Exito(convenios.ToDto());
            }
            else if (_Dto.convenioType == "especifico")
            {
                var query = _UnitOfWork._ConvenioEspecificoRepository.GetQueryByFiltering();

                var convenios = await query
                    .Where(c => c.FechaFinConvenio != null).AsNoTracking()
                    .OrderByDescending(c => c.FechaFinConvenio).Take(30).ToListAsync();

                if (convenios.Count == 0) return Result<object>.
                        Error("no hay convenios marcos registrados", 404);

                return Result<object>.Exito(convenios.ToDto());
            }
            else
            {
                var context1 = _UnitOfWork._ContextFactory.CreateDbContext();
                var context2 = _UnitOfWork._ContextFactory.CreateDbContext();

                var Task1 = context1.ConveniosMarcos.Where(c => c.FechaFin != null)
                    .OrderByDescending(c => c.FechaFin).Take(30).ToListAsync();

                var task2 = context2.ConveniosEspecificos.Where(c => c.FechaFinConvenio != null)
                    .OrderByDescending(c => c.FechaFinConvenio).Take(30).ToListAsync();

                await Task.WhenAll(Task1, task2);

                var conveniosMarco = await Task1;
                var conveniosEspecificos = await task2;

                if (conveniosMarco.Count == 0 && conveniosEspecificos.Count == 0) 
                    return Result<object>.Error("no hay convenios que coincidan con la busqueda", 404);


                var Data = new ListConveniosDto
                {
                    conveniosMarcos = conveniosMarco.ToDto(),
                    convenioEspecificos = conveniosEspecificos.ToDto(),
                };

                
                return Result<object>.Exito(Data);
            }
        }
    }
}
