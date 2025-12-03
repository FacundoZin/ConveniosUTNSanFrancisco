using APIconvenios.Common;
using APIconvenios.DTOs.Convenios;
using APIconvenios.DTOs.Filters;
using APIconvenios.Helpers.Mappers;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Commands.FilterCommands.Commands
{
    public class SearchByFechaFirmaCmd : IFilterCommands
    {
        private readonly ByFechaFirmaDto _Dto;
        public SearchByFechaFirmaCmd(ByFechaFirmaDto fechaFirma)
        {
            _Dto = fechaFirma;
        }

        public async Task<Result<object>> ExecuteAsync(_UnitOfWork _UnitOfWork)
        {
            if (_Dto.convenioType == "marco")
            {
                var query = _UnitOfWork._ConvenioMarcoRepository.GetQueryByFiltering();
                var convenios = await query.Where(c => c.FechaFirmaConvenio == _Dto.FechaInicio)
                    .ToListAsync();

                if (convenios.Count == 0) return Result<object>.
                        Error("No se encontraron convenios marco con la fecha de firma especificada.", 404);

                return Result<object>.Exito(convenios.ToDto());
            }
            else if (_Dto.convenioType == "especifico")
            {
                var query = _UnitOfWork._ConvenioEspecificoRepository.GetQueryByFiltering();
                var convenios = await query.Where(c => c.FechaFirmaConvenio == _Dto.FechaInicio)
                    .ToListAsync();

                if (convenios.Count == 0) return Result<object>.
                        Error("No se encontraron convenios especificos con la fecha de firma especificada.", 404);

                return Result<object>.Exito(convenios.ToDto());
            }
            else
            {
                var context1 = await _UnitOfWork._ContextFactory.CreateDbContextAsync();
                var context2 = await _UnitOfWork._ContextFactory.CreateDbContextAsync();

                var task1 = context1.ConveniosEspecificos.Where(c => c.FechaFirmaConvenio == _Dto.FechaInicio)
                    .ToListAsync();

                var task2 = context2.ConveniosMarcos.Where(c => c.FechaFirmaConvenio == _Dto.FechaInicio)
                    .ToListAsync();


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
