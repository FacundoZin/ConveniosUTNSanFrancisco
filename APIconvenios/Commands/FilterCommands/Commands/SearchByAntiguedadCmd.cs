using APIconvenios.Common;
using APIconvenios.DTOs.Filters;
using APIconvenios.Helpers.Mappers;
using APIconvenios.Models;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace APIconvenios.Commands.FilterCommands.Commands
{
    public class SearchByAntiguedadCmd : IFilterCommands
    {
        private readonly ByAntiguedadDto _Dto;

        public SearchByAntiguedadCmd(ByAntiguedadDto dto)
        {
            _Dto = dto;
        }

        public async Task<Result<object>> ExecuteAsync(_UnitOfWork _UnitOfWork)
        {
            if (_Dto.convenioType == "marco")
            {
                var query = _UnitOfWork._ConvenioMarcoRepository.GetQuery();

                var convenios = new List<APIconvenios.Models.ConvenioMarco>();

                if (_Dto.ascendente)
                    convenios = await query
                        .Where(c => c.FechaFirmaConvenio != null)
                        .Include(c => c.Empresa)
                        .OrderBy(c => c.FechaFirmaConvenio).Take(30).ToListAsync();

                convenios = await query.OrderByDescending(c => c.FechaFirmaConvenio).Take(30).ToListAsync();

                if (convenios.Count == 0) Result<object>.Error("no hay convenios marcos registrados", 404);

                return Result<object>.Exito(convenios.ToDto());
            }
            else
            {
                var query = _UnitOfWork._ConvenioEspecificoRepository.GetQuery();

                var convenios = new List<APIconvenios.Models.ConvenioEspecifico>();

                if (_Dto.ascendente)
                    convenios = await query
                        .Where(c => c.FechaFirmaConvenio != null)
                        .Include(c => c.empresa)
                        .OrderBy(c => c.FechaFirmaConvenio).Take(30).ToListAsync();

                convenios = await query.OrderByDescending(c => c.FechaFirmaConvenio).Take(30).ToListAsync();

                if (convenios.Count == 0) Result<object>.Error("no hay convenios especificos registrados", 404);

                return Result<object>.Exito(convenios.ToDto());
            }
        }
    }
}
